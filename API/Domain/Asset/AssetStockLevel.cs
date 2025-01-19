using API.Domain.Shared;

namespace API.Domain.Asset;

public class AssetStockLevel : TenantEntity
{
    public required string AssetId { get; set; }
    public Asset? Asset { get; set; }

    public int StockLevel { get; set; }
}