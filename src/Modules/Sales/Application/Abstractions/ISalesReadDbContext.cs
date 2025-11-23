using TempooERP.Modules.Sales.Domain.Orders;

namespace TempooERP.Modules.Sales.Application.Abstractions;

public interface ISalesReadDbContext
{
    IQueryable<Order> Orders { get; }
    IQueryable<OrderLine> OrderLines { get; }
}
