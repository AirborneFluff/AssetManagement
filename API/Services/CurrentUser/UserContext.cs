using System.Security.Claims;
using API.Domain.Authentication.Constants;

namespace API.Services.CurrentUser;

public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public string UserId => httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)!;
    public string? TenantId => httpContextAccessor.HttpContext?.User.FindFirstValue(CustomClaimTypes.TenantId);
    public string Email => httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email)!;
}