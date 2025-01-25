using API.Domain.Authentication.Dtos;
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
    IMediator mediator)
    : IRequestHandler<LoginUserCommand, Result<AppUserDto>>
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
        catch(Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }
}