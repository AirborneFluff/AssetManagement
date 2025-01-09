using System.Linq.Expressions;

namespace API.Extensions;

public static class IQueryableExtensions
{
    public static IQueryable<T> OrderByField<T>(this IQueryable<T> source, string sortField, string sortBy)
    {
        if (string.IsNullOrWhiteSpace(sortField) || string.IsNullOrWhiteSpace(sortBy))
        {
            return source;
        }
    
        var parameter = Expression.Parameter(typeof(T), "x");

        Expression property = parameter;
        foreach (var member in sortField.Split('.'))
        {
            property = Expression.PropertyOrField(property, member);
        }

        var lambda = Expression.Lambda(property, parameter);

        var method = sortBy.ToLower() == "descend" ? "OrderByDescending" : "OrderBy";

        var resultExpression = Expression.Call(
            typeof(Queryable),
            method,
            new Type[] { typeof(T), property.Type },
            source.Expression,
            Expression.Quote(lambda)
        );

        // Return the ordered query
        return source.Provider.CreateQuery<T>(resultExpression);
    }
    
    public static IQueryable<TEntity> WhereContains<TEntity>(
        this IQueryable<TEntity> query,
        Expression<Func<TEntity, string>> propertySelector,
        string? value
    )
        where TEntity : class
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return query;
        }

        var parameter = propertySelector.Parameters[0];
        var property = Expression.Call(
            propertySelector.Body,
            typeof(string).GetMethod("ToLower", Type.EmptyTypes)!
        );
        var lowerValue = value.ToLower();
        var condition = Expression.Call(
            property,
            typeof(string).GetMethod("Contains", new[] { typeof(string) })!,
            Expression.Constant(lowerValue)
        );

        var lambda = Expression.Lambda<Func<TEntity, bool>>(condition, parameter);

        return query.Where(lambda);
    }
}