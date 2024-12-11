using System.ComponentModel.DataAnnotations;
using API.Domain.Tenant;
using Microsoft.AspNetCore.Identity;

namespace API.Domain.Authentication;

public sealed class AppUser : IdentityUser
{
    [MaxLength(36)]
    public string? TenantId { get; set; }
    public AppTenant? Tenant { get; set; }
    
    public AppUser(string email, string? tenantId = null) : base(email)
    {
        Email = email;
        TenantId = tenantId;
    }
}