using API.Domain.Authentication.Constants;
using API.Services.Modules;

namespace API.Middleware;

public class TenantModulesMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context, ITenantModulesService modulesService)
    {
        var cancellationToken = context.RequestAborted; 
        var tenantId = context.User.FindFirst(CustomClaimTypes.TenantId)?.Value;
        var tenantModuleVersion = context.User.FindFirst(CustomClaimTypes.ModulesVersion)?.Value;

        if (tenantId == null || tenantModuleVersion == null)
        {
            await next(context);
            return;
        }
        
        var validVersion = await modulesService.IsModuleVersionValid(tenantId, tenantModuleVersion, cancellationToken);
        
        var routeValues = context.Request.RouteValues;
        /*if (routeValues.TryGetValue("tenantId", out var tenantIdInRoute))
        {
            var tenantIdClaim = context.User.FindFirst(CustomClaimTypes.TenantId)?.Value;

            if (!string.IsNullOrEmpty(tenantIdClaim) && tenantIdInRoute?.ToString() != tenantIdClaim)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Forbidden: Access to this resource is not allowed.");
                return;
            }
        }*/

        await next(context);
    }
}