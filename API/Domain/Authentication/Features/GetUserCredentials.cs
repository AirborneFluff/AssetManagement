using System.Collections.ObjectModel;
using System.Security.Claims;
using API.Data.Interfaces;
using API.Domain.Authentication.Constants;
using API.Domain.Authentication.Dtos;
using API.Domain.Modules;
using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Authentication.Features;

public record GetUserCredentialsCommand(AppUser User)
    : IRequest<Result<(AppUserDto userDto, ClaimsPrincipal principal)>>;

public class GetUserCredentialsHandler(
    IUnitOfWork unitOfWork,
    UserManager<AppUser> userManager,
    IMapper mapper
) : IRequestHandler<GetUserCredentialsCommand, Result<(AppUserDto userDto, ClaimsPrincipal principal)>>
{
    public async Task<Result<(AppUserDto userDto, ClaimsPrincipal principal)>>
        Handle(GetUserCredentialsCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var roles = await userManager.GetRolesAsync(request.User);
            var role = roles.Single();
            var (modules, modulesVersion) = await GetUserModulesVersionAsync(request.User.TenantId, cancellationToken);

            var claims = new Collection<Claim>
                {
                    new(ClaimTypes.NameIdentifier, request.User.Id),
                    new(ClaimTypes.Name, request.User.Email!),
                    new(ClaimTypes.Role, role),
                    new(CustomClaimTypes.TenantId, request.User.TenantId ?? string.Empty),
                    new(CustomClaimTypes.ModulesVersion, modulesVersion ?? string.Empty)
                };

            foreach (var module in modules)
            {
                claims.Add(new Claim(CustomClaimTypes.Modules, module.Identifier));
            }
        
            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            
            var userDto = mapper.Map<AppUserDto>(request.User, opt =>
            {
                opt.Items["Role"] = role;
                opt.Items["Modules"] = modules.Select(module => module.Identifier);
                opt.Items["ModulesVersion"] = modulesVersion;
            });

            return (userDto, claimsPrincipal);
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