using System.Security.Claims;
using API.Domain.Authentication.Constants;

namespace API.Services.CurrentUser;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    public string UserId => httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)!;
    public string TenantId => httpContextAccessor.HttpContext?.User.FindFirstValue(CustomClaimTypes.TenantId)!;
    public string Email => httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email)!;
}