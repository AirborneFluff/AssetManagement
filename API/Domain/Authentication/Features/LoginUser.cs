using System.Collections.ObjectModel;
using System.Security.Claims;
using API.Data.Interfaces;
using API.Domain.Authentication.Constants;
using API.Domain.Authentication.Dtos;
using API.Domain.Modules;
using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Authentication.Features;

public record LoginUserCommand(string Email, string Password) : IRequest<Result<AppUserDto>>;

public class LoginUserHandler(
    IUnitOfWork unitOfWork,
    UserManager<AppUser> userManager,
    IHttpContextAccessor httpContextAccessor,
    IMediator mediator
) : IRequestHandler<LoginUserCommand, Result<AppUserDto>>
{
    public async Task<Result<AppUserDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await userManager.FindByEmailAsync(request.Email);
            if (user is null || !await userManager.CheckPasswordAsync(user, request.Password))
            {
                return Result.Fail("Invalid username or password");
            }
            
            var response = await mediator.Send(new GetUserCredentialsCommand(user), cancellationToken);
            if (response.IsFailed) return Result.Fail(response.Errors);
            var (userDto, principal) = response.Value;

            if (httpContextAccessor.HttpContext is null)
            {
                return Result.Fail("HttpContext is null");
            }
            
            await httpContextAccessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme, 
                principal);

            return userDto;
        }
        catch(Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    private async Task<(List<AppModule>, string?)> GetUserModulesVersionAsync(string? tenantId, CancellationToken cancellationToken)
    {
        var tenant = await unitOfWork.Context.Tenants
            .Include(t => t.Modules)
            .Where(t => t.Id == tenantId)
            .FirstOrDefaultAsync(cancellationToken);

        if (tenant is not null) return (tenant.Modules, tenant.ModulesVersion);
        
        var allModules = await unitOfWork.Context.Modules.ToListAsync(cancellationToken);
        return (allModules, null);

    }
}