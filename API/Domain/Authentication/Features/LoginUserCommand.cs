using System.Collections.ObjectModel;
using System.Security.Claims;
using API.Domain.Authentication.Constants;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;

namespace API.Domain.Authentication.Features;

public record LoginUserCommand(string Email, string Password) : IRequest<Result<ClaimsPrincipal>>;

public class LoginUserHandler(UserManager<AppUser> userManager) : IRequestHandler<LoginUserCommand, Result<ClaimsPrincipal>>
{
    public async Task<Result<ClaimsPrincipal>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await userManager.FindByEmailAsync(request.Email);
            if (user is null || !await userManager.CheckPasswordAsync(user, request.Password))
            {
                return Result.Fail("Invalid username or password");
            }

            var roles = await userManager.GetRolesAsync(user);

            var claims = roles
                .Select(role => new Claim(ClaimTypes.Role, role))
                .Union(new Collection<Claim>
                {
                    new(ClaimTypes.NameIdentifier, user.Id),
                    new(ClaimTypes.Name, user.UserName!),
                    new(CustomClaimTypes.TenantId, user.TenantId ?? string.Empty)
                });
        
            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            return new ClaimsPrincipal(claimsIdentity);
        }
        catch(Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }
}