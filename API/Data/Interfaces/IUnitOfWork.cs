using API.Domain.Asset.Repositories;

namespace API.Data.Interfaces;

public interface IUnitOfWork
{
    DataContext Context { get; }
    IAssetCategoryRepository AssetCategoryRepository { get; }

    Task<bool> Complete();
    bool HasChanges();
}