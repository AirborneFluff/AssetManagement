using API.Domain.Shared;

namespace API.Domain.Asset;

public class AssetTag : TenantEntity
{
    public required string AssetId { get; set; }
    public Asset? Asset { get; set; }
    
    public required string Tag { get; set; }
}