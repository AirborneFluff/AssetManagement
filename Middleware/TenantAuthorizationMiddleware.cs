using API.Domain.Authentication.Constants;

namespace API.Middleware;

public class TenantAuthorizationMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        var routeValues = context.Request.RouteValues;
        if (routeValues.TryGetValue("tenantId", out var tenantIdInRoute))
        {
            var tenantIdClaim = context.User.FindFirst(CustomClaimTypes.TenantId)?.Value;

            if (!string.IsNullOrEmpty(tenantIdClaim) && tenantIdInRoute?.ToString() != tenantIdClaim)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Forbidden: Access to this resource is not allowed.");
                return;
            }
        }

        await next(context);
    }
}