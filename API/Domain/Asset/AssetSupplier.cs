using API.Domain.Shared;

namespace API.Domain.Asset;

public class AssetSupplier : TenantEntity
{
    public required string Name { get; set; }
    public string? Website { get; set; }
}