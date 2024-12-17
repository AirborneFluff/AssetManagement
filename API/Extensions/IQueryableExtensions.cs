using System.Linq.Expressions;

namespace API.Extensions;

public static class IQueryableExtensions
{
    public static IQueryable<T> OrderByField<T>(this IQueryable<T> source, string sortField, string sortBy)
    {
        if (String.IsNullOrWhiteSpace(sortField) || String.IsNullOrWhiteSpace(sortBy))
        {
            return source;
        }
        
        var parameter = Expression.Parameter(typeof(T), "x");
        var property = Expression.PropertyOrField(parameter, sortField);
        var lambda = Expression.Lambda(property, parameter);

        var method = sortBy.ToLower() == "descend" ? "OrderByDescending" : "OrderBy";
        var resultExpression = Expression.Call(typeof(Queryable), method, [typeof(T), property.Type],
            source.Expression, Expression.Quote(lambda));

        return source.Provider.CreateQuery<T>(resultExpression);
    }
}