using API.Domain.Shared;

namespace API.Domain.Asset;

public class AssetSupplySource : BaseEntity
{
    public required string AssetId { get; set; }
    public Asset? Asset { get; set; }
    
    public required string SupplierId { get; set; }
    public AssetSupplier? Supplier { get; set; }

    public required string SupplierReference { get; set; }
    public string? QuantityUnit { get; set; }

    public List<AssetSupplySourcePrice> Prices { get; set; } = [];
}