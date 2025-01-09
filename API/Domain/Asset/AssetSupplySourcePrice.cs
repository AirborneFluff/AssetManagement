using API.Domain.Shared;

namespace API.Domain.Asset;

public class AssetSupplySourcePrice : BaseEntity
{
    public required string SupplySourceId { get; set; }
    public AssetSupplySource? SupplySource { get; set; }
    
    public double UnitPrice { get; set; }
    public float Quantity { get; set; }
}