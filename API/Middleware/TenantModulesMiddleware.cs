using System.Security.Claims;
using API.Domain.Authentication;
using API.Domain.Authentication.Constants;
using API.Domain.Authentication.Features;
using API.Services.Modules;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;

namespace API.Middleware;

public class TenantModulesMiddleware(RequestDelegate next)
{
    public async Task Invoke(
        HttpContext context, 
        ITenantModulesService modulesService,
        IMediator mediator,
        UserManager<AppUser> userManager)
    {
        var requestCancellationToken = context.RequestAborted;

        if (!TryGetTenantClaims(context, out var tenantId, out var tenantModuleVersion))
        {
            await next(context);
            return;
        }

        if (await modulesService.IsModuleVersionValid(tenantId!, tenantModuleVersion!, requestCancellationToken))
        {
            await next(context);
            return;
        }

        var userEmail = context.User.FindFirst(ClaimTypes.Name)?.Value;
        if (string.IsNullOrEmpty(userEmail))
        {
            await RespondUnauthorized(context, "Invalid Credentials", requestCancellationToken);
            return;
        }

        var user = await userManager.FindByEmailAsync(userEmail);
        if (user is null)
        {
            await RespondUnauthorized(context, "Invalid Credentials", requestCancellationToken);
            return;
        }

        var credentialsResponse = await mediator.Send(new GetUserCredentialsCommand(user), requestCancellationToken);
        if (credentialsResponse.IsFailed)
        {
            await RespondUnauthorized(context, "Invalid Credentials", requestCancellationToken);
            return;
        }

        await SignInUser(context, credentialsResponse.Value.principal);
        await next(context);
    }

    private static bool TryGetTenantClaims(HttpContext context, out string? tenantId, out string? tenantModuleVersion)
    {
        tenantId = context.User.FindFirst(CustomClaimTypes.TenantId)?.Value;
        tenantModuleVersion = context.User.FindFirst(CustomClaimTypes.ModulesVersion)?.Value;
        return !String.IsNullOrWhiteSpace(tenantId) && !String.IsNullOrWhiteSpace(tenantModuleVersion);
    }

    private static async Task RespondUnauthorized(HttpContext context, string message, CancellationToken cancellationToken)
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        await context.Response.WriteAsync(message, cancellationToken);
    }

    private static async Task SignInUser(HttpContext context, ClaimsPrincipal principal)
    {
        await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
    }
}