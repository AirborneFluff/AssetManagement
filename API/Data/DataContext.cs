using System.Linq.Expressions;
using System.Reflection;
using API.Data.Converters;
using API.Domain.Asset;
using API.Domain.Authentication;
using API.Domain.Shared;
using API.Domain.Tenant;
using API.Extensions;
using API.Services.CurrentUser;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class DataContext(DbContextOptions<DataContext> options, IUserContext userContext)
    : IdentityDbContext<AppUser>(options)
{
    public DbSet<AppTenant> Tenants { get; set; }
    public DbSet<Asset> Assets { get; set; }
    public DbSet<AssetTag> AssetTags { get; set; }
    public DbSet<AssetCategory> AssetCategories { get; set; }
    public DbSet<AssetSupplier> AssetSuppliers { get; set; }
    public DbSet<AssetStockLevel> AssetStockLevels { get; set; }
    public DbSet<AssetSupplySource> AssetSupplySources { get; set; }
    
    protected IUserContext userContext { get; set; } = userContext;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        ApplyGlobalFilters(modelBuilder);
        
        modelBuilder.Entity<AssetSupplySource>(entity =>
        {
            entity.Property(e => e.Prices)
                .HasConversion(new DictionaryConverter<float, double>());
        });
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
    
    private void ApplyGlobalFilters(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var isBaseEntity = typeof(BaseEntity).IsAssignableFrom(entityType.ClrType);
            var isAuditEntity = typeof(AuditEntity).IsAssignableFrom(entityType.ClrType);

            if (!isBaseEntity && !isAuditEntity) continue;

            var parameter = Expression.Parameter(entityType.ClrType, "e");
            Expression? combinedFilter = null;

            if (isBaseEntity)
            {
                var tenantIdProperty = Expression.Property(parameter, "TenantId");

                var tenantIdAccessor = Expression.MakeMemberAccess(
                    Expression.Constant(this, GetType()),
                    GetType().GetProperty(nameof(userContext), BindingFlags.Instance | BindingFlags.NonPublic)!
                );

                var tenantIdValue = Expression.MakeMemberAccess(
                    tenantIdAccessor,
                    typeof(IUserContext).GetProperty(nameof(IUserContext.TenantId))!
                );

                var tenantFilter = Expression.Equal(tenantIdProperty, tenantIdValue);
                combinedFilter = combinedFilter == null ? tenantFilter : Expression.AndAlso(combinedFilter, tenantFilter);
            }

            if (isAuditEntity)
            {
                var isDeletedProperty = Expression.Property(parameter, "IsDeleted");
                var isDeletedFilter = Expression.Equal(isDeletedProperty, Expression.Constant(false));

                combinedFilter = combinedFilter == null ? isDeletedFilter : Expression.AndAlso(combinedFilter, isDeletedFilter);
            }

            if (combinedFilter == null) continue;

            var filterExpression = Expression.Lambda(combinedFilter, parameter);
            modelBuilder.Entity(entityType.ClrType).HasQueryFilter(filterExpression);
        }
    }
}