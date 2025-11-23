
using TempooERP.Modules.Sales.Application.Abstractions;
using TempooERP.Modules.Sales.Domain.Orders;

namespace TempooERP.Infrastructure.Data.Modules.Sales;

/// <summary>
/// Adapter exposing write operations required by the application layer.
/// </summary>
public sealed class SalesWriteDbContextAdapter(ErpDbContext db) : WriteDbContext(db), ISalesWriteDbContext
{
    private readonly ErpDbContext _db = db;

    public IQueryable<Order> Orders => _db.Orders;
    public IQueryable<OrderLine> OrderLines => _db.OrderLines;
}
