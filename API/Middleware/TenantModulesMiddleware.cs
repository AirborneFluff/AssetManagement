using API.Attributes;
using API.Domain.Authentication.Constants;

namespace API.Middleware;

public class TenantModulesMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        var endpoint = context.GetEndpoint();
        var moduleAttribute = endpoint?.Metadata.GetMetadata<ModuleAuthorizationAttribute>();
        if (moduleAttribute is null)
        {
            await next(context);
            return;
        }
        
        var moduleClaims = context.User
            .FindAll(CustomClaimTypes.Modules)
            .Select(c => c.Value);

        foreach (var moduleClaim in moduleClaims)
        {
            if (moduleClaim != moduleAttribute.ModuleIdentity) continue;
            
            await next(context);
            return;
        }
        
        context.Response.StatusCode = StatusCodes.Status403Forbidden;
        await context.Response.WriteAsync($"Access denied for module: {moduleAttribute.ModuleIdentity}");
    }
}