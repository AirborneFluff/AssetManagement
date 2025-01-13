namespace API.Domain.Asset.Dto.SupplySources;

public class NewAssetSupplySourceDto
{
    public required string SupplierId { get; set; }
    public required string SupplierReference { get; set; }
    public string? QuantityUnit { get; set; }

    public Dictionary<float, double> Prices { get; set; } = [];
}