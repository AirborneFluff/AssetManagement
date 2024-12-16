using API.Domain.Asset;
using API.Domain.Authentication;
using API.Domain.Shared;
using API.Domain.Tenant;
using API.Services.CurrentUser;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class DataContext(
    DbContextOptions<DataContext> options,
    IUserContext userContext
) : IdentityDbContext<AppUser>(options)
{
    public DbSet<AppTenant> Tenants { get; set; }
    public DbSet<Asset> Assets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ApplyEntityFilters<BaseEntity>(modelBuilder, nameof(SetTenantFilter));
        ApplyEntityFilters<AuditEntity>(modelBuilder, nameof(SetIsDeletedFilter));

        base.OnModelCreating(modelBuilder);
    }
    
    private void ApplyEntityFilters<TEntity>(ModelBuilder modelBuilder, string methodName)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (!typeof(TEntity).IsAssignableFrom(entityType.ClrType)) continue;
            var method = GetType()
                .GetMethod(methodName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.MakeGenericMethod(entityType.ClrType);

            method?.Invoke(this, [modelBuilder]);
        }
    }

    private void SetTenantFilter<TEntity>(ModelBuilder builder) where TEntity : BaseEntity
    {
        builder.Entity<TEntity>().HasQueryFilter(e => e.TenantId == userContext.TenantId);
    }

    private void SetIsDeletedFilter<TEntity>(ModelBuilder builder) where TEntity : AuditEntity
    {
        builder.Entity<TEntity>().HasQueryFilter(entity => !entity.IsDeleted);
    }

    public override int SaveChanges()
    {
        ApplyAuditFields();
        ApplyTenantIdField();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplyAuditFields();
        ApplyTenantIdField();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void ApplyAuditFields()
    {
        var userId = userContext.TenantId;
        var entries = ChangeTracker.Entries<AuditEntity>();

        foreach (var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedOn = DateTime.UtcNow;
                    entry.Entity.CreatedBy = userId;
                    break;
                case EntityState.Modified:
                    entry.Entity.ModifiedOn = DateTime.UtcNow;
                    entry.Entity.ModifiedBy = userId;
                    break;
                case EntityState.Deleted:
                    entry.Entity.DeletedOn = DateTime.UtcNow;
                    entry.Entity.DeletedBy = userId;
                    entry.Entity.IsDeleted = true;
                    entry.State = EntityState.Modified;
                    break;
            }
        }
    }

    private void ApplyTenantIdField()
    {
        var tenantId = userContext.TenantId;
        var entries = ChangeTracker.Entries<BaseEntity>();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.TenantId = tenantId;
            }
        }
    }
}