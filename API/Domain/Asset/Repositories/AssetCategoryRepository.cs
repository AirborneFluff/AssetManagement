using API.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Asset.Repositories;

public class AssetCategoryRepository(DataContext context) : IAssetCategoryRepository
{
    public Task<AssetCategory?> GetAssetCategoryByNameAsync(string name)
    {
        return context.AssetCategories
            .FirstOrDefaultAsync(c => EF.Functions.Like(c.Name, name));
    }

    public Task<bool> AssetCategoryExistsAsync(string name)
    {
        return context.AssetCategories
            .AnyAsync(c => EF.Functions.Like(c.Name, name));
    }
}