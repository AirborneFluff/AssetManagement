namespace API.Data;

public class UnitOfWork(DataContext dataContext)
{
    public DataContext Context { get; } = dataContext;

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