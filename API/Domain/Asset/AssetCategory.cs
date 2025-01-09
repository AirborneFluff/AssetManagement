using API.Domain.Shared;

namespace API.Domain.Asset;

public class AssetCategory : BaseEntity
{
    public required string Name { get; set; }
}