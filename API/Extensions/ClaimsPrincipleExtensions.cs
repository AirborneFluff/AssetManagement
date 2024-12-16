using System.Security.Claims;
using API.Domain.Authentication.Constants;

namespace API.Extensions;

public static class ClaimsPrincipleExtensions
{
    public static string GetId(this ClaimsPrincipal user)
    {
        return user.Claims.Single(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
    }

    public static string GetEmail(this ClaimsPrincipal user)
    {
        return user.Claims.Single(claim => claim.Type == ClaimTypes.Name).Value;
    }

    public static string GetRole(this ClaimsPrincipal user)
    {
        return user.Claims.Single(claim => claim.Type == ClaimTypes.Role).Value;
    }

    public static string GetTenantId(this ClaimsPrincipal user)
    {
        return user.Claims.Single(claim => claim.Type == CustomClaimTypes.TenantId).Value;
    }
}