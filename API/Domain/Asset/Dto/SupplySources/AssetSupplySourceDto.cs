namespace API.Domain.Asset.Dto.SupplySources;

public class AssetSupplySourceDto
{
    public required string Id { get; set; }
    public required string SupplierId { get; set; }
    public required string SupplierReference { get; set; }
    public string? QuantityUnit { get; set; }
}