namespace API.Domain.Asset.Dto;

public class NewAssetDto
{
    public required string Description { get; set; }
    public required string CategoryId { get; set; }
    public List<string> Tags { get; set; } = [];
}