namespace API.Domain.Asset.Dto;

public class AssetDto
{
    public required string Id { get; set; }
    public required string Description { get; set; }
    public List<AssetTagDto> Tags { get; set; } = [];

    public AssetCategoryDto? Category { get; set; }
}