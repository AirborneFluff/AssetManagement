namespace API.Domain.Shared.Params;

public class SortableParams : BasePaginationParams
{
    public string SortOrder { get; set; } = String.Empty;
    public string SortField { get; set; } = String.Empty;
}