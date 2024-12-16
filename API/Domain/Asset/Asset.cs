using API.Domain.Shared;

namespace API.Domain.Asset;

public class Asset : BaseEntity
{
    public required string Description { get; set; }
    public List<AssetTag> Tags { get; set; } = [];
}