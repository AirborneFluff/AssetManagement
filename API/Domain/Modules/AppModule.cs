using API.Domain.Shared;
using API.Domain.Tenant;

namespace API.Domain.Modules;

public class AppModule : BaseEntity
{
    public required string DisplayName { get; set; }
    public required string Identifier { get; set; }

    public IList<AppTenant> Tenants { get; set; } = [];
}