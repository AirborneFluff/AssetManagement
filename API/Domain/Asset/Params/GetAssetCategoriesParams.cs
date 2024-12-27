using API.Domain.Shared.Params;

namespace API.Domain.Asset.Params;

public class GetAssetCategoriesParams : SortableParams
{
    public string? Name { get; set; }
}