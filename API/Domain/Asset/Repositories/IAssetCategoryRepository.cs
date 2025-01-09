namespace API.Domain.Asset.Repositories;

public interface IAssetCategoryRepository
{
    Task<AssetCategory?> GetAssetCategoryByNameAsync(string name);
    Task<bool> AssetCategoryExistsAsync(string name);
}