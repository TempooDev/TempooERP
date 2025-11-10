using System.Linq.Expressions;

namespace TempooERP.BuildingBlocks.Application.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<T> ApplyOrdering<T>(
        this IQueryable<T> query,
        string? sortBy,
        string? sortDirection,
        string? defaultSort = "Id")
    {
        if (string.IsNullOrWhiteSpace(sortBy))
        {
            sortBy = defaultSort;
        }

        var direction = (sortDirection ?? "asc").ToLowerInvariant();

        var property = typeof(T).GetProperty(
            sortBy!,
            System.Reflection.BindingFlags.IgnoreCase |
            System.Reflection.BindingFlags.Public |
            System.Reflection.BindingFlags.Instance);

        if (property == null)
        {
            return query; // si la propiedad no existe, no ordena
        }

        var parameter = Expression.Parameter(typeof(T), "x");
        var propertyAccess = Expression.MakeMemberAccess(parameter, property);
        var orderByExp = Expression.Lambda(propertyAccess, parameter);

        string methodName = direction == "desc" ? "OrderByDescending" : "OrderBy";

        var resultExp = Expression.Call(
            typeof(Queryable),
            methodName,
            [typeof(T), property.PropertyType],
            query.Expression,
            Expression.Quote(orderByExp));

        return query.Provider.CreateQuery<T>(resultExp);
    }
}
