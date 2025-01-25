using System.Security.Claims;
using API.Domain.Authentication.Constants;

namespace API.Domain.Authentication.Dtos;

public class AppUserDto
{
    public required string Id { get; set; }
    public required string Email { get; set; }
    public string? TenantId { get; set; }
    public string? Role { get; set; }
    public string? ModulesVersion { get; set; }

    public IEnumerable<string> Modules { get; set; } = [];

    public static AppUserDto FromClaims(ClaimsPrincipal user) => new()
    {
        Id = user.FindFirstValue(ClaimTypes.NameIdentifier) ?? "",
        Email = user.FindFirstValue(ClaimTypes.Name) ?? "",
        TenantId = user.FindFirstValue(CustomClaimTypes.TenantId),
        Role = user.FindFirstValue(ClaimTypes.Role),
        ModulesVersion = user.FindFirstValue(CustomClaimTypes.ModulesVersion),
        Modules = user.FindAll(CustomClaimTypes.Modules).Select(c => c.Value)
    };
}