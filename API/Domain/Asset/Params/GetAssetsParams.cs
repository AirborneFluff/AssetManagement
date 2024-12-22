using API.Domain.Shared.Params;

namespace API.Domain.Asset.Params;

public class GetAssetsParams : SortableParams
{
    public string? Description { get; set; }
}