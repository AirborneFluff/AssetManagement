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

    [MaxLength(36)]
    public required string ModulesVersion { get; set; } = Guid.NewGuid().ToString();

    public List<AppUser> Users { get; set; } = [];
    public List<AppModule> Modules { get; set; } = [];

    public void RefreshModulesVersion()
    {
        ModulesVersion = Guid.NewGuid().ToString();
    }
}