using API.Domain.Authentication.Dtos;
using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;

namespace API.Domain.Authentication.Features;

public record SwitchTenantCommand(string UserId, string TenantId) : IRequest<Result<AppUserDto>>;

public class SwitchTenantHandler(
    UserManager<AppUser> userManager,
    IHttpContextAccessor httpContextAccessor,
    IMediator mediator)
    : IRequestHandler<SwitchTenantCommand, Result<AppUserDto>>
{
    public async Task<Result<AppUserDto>> Handle(SwitchTenantCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.UserId);
        if (user is null) return Result.Fail("User not found");
        
        var response = await mediator.Send(new GetUserCredentialsCommand(user, request.TenantId), cancellationToken);
        if (response.IsFailed) return Result.Fail(response.Errors);
        var principal = response.Value;

        if (httpContextAccessor.HttpContext is null)
        {
            return Result.Fail("HttpContext is null");
        }
            
        await httpContextAccessor.HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme, 
            principal);

        return AppUserDto.FromClaims(principal);
    }
}