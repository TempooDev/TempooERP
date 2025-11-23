
using TempooERP.Modules.Sales.Application.Abstractions;
using TempooERP.Modules.Sales.Domain.Orders;

namespace TempooERP.Infrastructure.Data.Modules.Sales;

/// <summary>
/// Adapter exposing read-only IQueryable surfaces required by application layer.
/// </summary>
public sealed class SalesWriteDbContextAdapter(ErpDbContext db) : WriteDbContext(db), ISalesWriteDbContext
{
    private readonly ErpDbContext _db = db;

    public IQueryable<Order> Orders => _db.Orders;
    public IQueryable<OrderLine> OrderLines => _db.OrderLines;
}
