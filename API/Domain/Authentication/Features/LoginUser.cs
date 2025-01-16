using System.Collections.ObjectModel;
using System.Security.Claims;
using API.Domain.Authentication.Constants;
using API.Domain.Authentication.Dtos;
using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;

namespace API.Domain.Authentication.Features;

public record LoginUserCommand(string Email, string Password) : IRequest<Result<AppUserDto>>;

public class LoginUserHandler(
    UserManager<AppUser> userManager,
    IHttpContextAccessor httpContextAccessor,
    IMapper mapper
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

            var roles = await userManager.GetRolesAsync(user);
            var role = roles.Single();

            var claims = new Collection<Claim>
                {
                    new(ClaimTypes.NameIdentifier, user.Id),
                    new(ClaimTypes.Name, user.Email!),
                    new(ClaimTypes.Role, role),
                    new(CustomClaimTypes.TenantId, user.TenantId ?? string.Empty)
                };
        
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
            });

            return userDto;
        }
        catch(Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }
}