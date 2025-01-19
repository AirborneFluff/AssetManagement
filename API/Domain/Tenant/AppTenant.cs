using System.ComponentModel.DataAnnotations;
using API.Domain.Authentication;
using API.Domain.Modules;
using API.Domain.Shared;

namespace API.Domain.Tenant;

public class AppTenant : AuditEntity
{
    [MaxLength(128)]
    public required string Name { get; set; }

    [Range(1, int.MaxValue)]
    public int Licences { get; set; }

    public List<AppUser> Users { get; set; } = [];
    public List<AppModule> Modules { get; set; } = [];
}