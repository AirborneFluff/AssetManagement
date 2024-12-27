using API.Data.Interfaces;
using API.Domain.Asset.Repositories;

namespace API.Data;

public class UnitOfWork(DataContext dataContext) : IUnitOfWork
{
    public DataContext Context { get; } = dataContext;
    public IAssetCategoryRepository AssetCategoryRepository => new AssetCategoryRepository(Context);

    public async Task<bool> Complete()
    {
        try { return await Context.SaveChangesAsync() > 0; }
        catch { return false; }
    }

    public bool HasChanges()
    {
        return Context.ChangeTracker.HasChanges();
    }
}