using System.Linq.Expressions;
using API.Domain.Shared;
using API.Services.CurrentUser;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyGlobalFilters(this ModelBuilder modelBuilder, IUserContext userContext)
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

                var tenantIdValue = Expression.Constant(userContext, userContext.GetType());
                var tenantIdAccessor = Expression.Property(tenantIdValue, nameof(userContext.TenantId));

                var tenantFilter = Expression.Equal(tenantIdProperty, tenantIdAccessor);
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