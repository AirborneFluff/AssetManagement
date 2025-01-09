using API.Domain.Shared;

namespace API.Domain.Asset;

public class Asset : BaseEntity
{
    public required string Description { get; set; }
    public List<AssetTag> Tags { get; set; } = [];

    public required string CategoryId { get; set; }
    public AssetCategory? Category { get; set; }

    public List<AssetSupplySource> SupplySources { get; set; } = [];
}