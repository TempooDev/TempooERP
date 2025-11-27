using System.Linq.Expressions;
using TempooERP.BuildingBlocks.Application.Abstractions;
using TempooERP.Modules.Sales.Application.Orders;
using TempooERP.Modules.Sales.Domain.Orders;

namespace TempooERP.Modules.Sales.Application.Abstractions;

/// <summary>
/// Repository abstraction for read operations on Orders.
/// </summary>
public interface IOrderReadRepository
{
    Task<PagedResult<OrderDto>> SearchAsync(
        Expression<Func<Order, bool>> predicate,
        int page,
        int pageSize,
        string? sortBy,
        string? sortDirection,
        CancellationToken ct = default);
}