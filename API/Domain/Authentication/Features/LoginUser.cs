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
    IMapper mapper
) : IRequestHandler<LoginUserCommand, Result<AppUserDto>>
{
    public async Task<Result<AppUserDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // create a new feature which gets the user and creates claims
            // Or leave as is, and in the module middleware, reconstruct the claims
            // by using all the current user data and add new modules
            var user = await userManager.FindByEmailAsync(request.Email);
            if (user is null || !await userManager.CheckPasswordAsync(user, request.Password))
            {
                return Result.Fail("Invalid username or password");
            }

            var roles = await userManager.GetRolesAsync(user);
            var role = roles.Single();
            var (modules, modulesVersion) = await GetUserModulesVersionAsync(user.TenantId, cancellationToken);

            var claims = new Collection<Claim>
                {
                    new(ClaimTypes.NameIdentifier, user.Id),
                    new(ClaimTypes.Name, user.Email!),
                    new(ClaimTypes.Role, role),
                    new(CustomClaimTypes.TenantId, user.TenantId ?? string.Empty),
                    new(CustomClaimTypes.ModulesVersion, modulesVersion ?? string.Empty)
                };

            foreach (var module in modules)
            {
                claims.Add(new Claim(CustomClaimTypes.Modules, module.Identifier));
            }
        
            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            if (httpContextAccessor.HttpContext is null)
            {
                return Result.Fail("HttpContext is null");
            }
            
            await httpContextAccessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme, 
                new ClaimsPrincipal(claimsIdentity));
            
            var userDto = mapper.Map<AppUserDto>(user, opt =>
            {
                opt.Items["Role"] = role;
                opt.Items["Modules"] = modules.Select(module => module.Identifier);
                opt.Items["ModulesVersion"] = modulesVersion;
            });

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