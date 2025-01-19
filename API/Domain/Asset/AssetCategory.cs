using API.Domain.Shared;

namespace API.Domain.Asset;

public class AssetCategory : TenantEntity
{
    public required string Name { get; set; }
}