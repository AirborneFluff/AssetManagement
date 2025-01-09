namespace API.Domain.Asset.Dto;

public class AssetSupplierDto
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public string? Website { get; set; }
}