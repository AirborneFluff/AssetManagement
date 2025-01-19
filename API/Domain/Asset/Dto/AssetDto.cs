using API.Domain.Asset.Dto.Categories;
using API.Domain.Asset.Dto.StockLevels;
using API.Domain.Asset.Dto.SupplySources;

namespace API.Domain.Asset.Dto;

public class AssetDto
{
    public required string Id { get; set; }
    public required string Description { get; set; }
    public List<AssetTagDto> Tags { get; set; } = [];

    public double StockLevel { get; set; }

    public AssetCategoryDto? Category { get; set; }
    public List<AssetSupplySourceDto> SupplySources { get; set; } = [];
    public List<AssetStockLevelDto> HistoricStockLevels { get; set; } = [];
}