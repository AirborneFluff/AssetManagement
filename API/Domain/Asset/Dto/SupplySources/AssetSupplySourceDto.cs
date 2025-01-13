namespace API.Domain.Asset.Dto.SupplySources;

public class AssetSupplySourceDto
{
    public required string Id { get; set; }
    public required string SupplierId { get; set; }
    public string? SupplierName { get; set; }
    
    public required string SupplierReference { get; set; }
    public string? QuantityUnit { get; set; }

    public Dictionary<float, double> Prices { get; set; } = [];
}