using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TempooERP.BuildingBlocks.Application.Abstractions;
using TempooERP.BuildingBlocks.Application.Extensions;
using TempooERP.Modules.Sales.Application.Abstractions;
using TempooERP.Modules.Sales.Application.Orders;
using TempooERP.Modules.Sales.Domain.Orders;

namespace TempooERP.Infrastructure.Repositories;

public sealed class OrderReadRepository(ISalesReadDbContext db) : IOrderReadRepository
{
    private readonly ISalesReadDbContext _db = db;

    public async Task<PagedResult<OrderDto>> SearchAsync(
        Expression<Func<Order, bool>> predicate,
        int page,
        int pageSize,
        string? sortBy,
        string? sortDirection,
        CancellationToken ct = default)
    {
        var query = _db.Orders
            .Where(predicate);

        query = query.ApplyOrdering(sortBy, sortDirection);

        var total = await query.CountAsync(ct);

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(o => new OrderDto(
                o.Id,
                o.UserId,
                o.Number,
                o.OrderLines.Sum(ol => ol.Quantity * ol.UnitPrice),
                o.Status.ToString()
                ))
            .ToListAsync(ct);

        return new PagedResult<OrderDto>(items, total, page, pageSize);
    }
}