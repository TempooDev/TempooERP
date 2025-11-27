
using System.Linq.Expressions;
using TempooERP.BuildingBlocks.Application.Abstractions;
using TempooERP.Modules.Sales.Application.Abstractions;
using TempooERP.Modules.Sales.Contracts;
using TempooERP.Modules.Sales.Domain.Orders;

namespace TempooERP.Modules.Sales.Application.Orders.Queries.GetByCriteria;

public sealed class GetOrderByCriteriaHandler(IOrderReadRepository orders)
: IQueryHandler<GetOrderByCriteriaQuery, PagedResult<OrderDto>>
{
    private readonly IOrderReadRepository _orders = orders;

    public async Task<PagedResult<OrderDto>> HandleAsync(
        GetOrderByCriteriaQuery query,
        CancellationToken cancellationToken)
    {
        var predicate = BuildPredicate(query);
        return await _orders.SearchAsync(
            predicate,
            query.Page,
            query.PageSize,
            query.SortBy,
            query.SortDirection,
            cancellationToken);
    }

    public static Expression<Func<Order, bool>> BuildPredicate(GetOrderByCriteriaQuery query)
    {
        var status = query.Status != null && OrderStatusExtensions.TryParse(query.Status, out var parsedStatus) ? parsedStatus : (OrderStatus?)null;

        return p =>
            (!status.HasValue || p.Status == status) &&
            (!query.CustomerId.HasValue || p.UserId == query.CustomerId.Value) &&
            (!query.TotalAmountLower.HasValue || p.OrderLines.Sum(ol => ol.UnitPrice * ol.Quantity) >= query.TotalAmountLower.Value) &&
            (!query.TotalAmountUpper.HasValue || p.OrderLines.Sum(ol => ol.UnitPrice * ol.Quantity) <= query.TotalAmountUpper.Value);
    }
}