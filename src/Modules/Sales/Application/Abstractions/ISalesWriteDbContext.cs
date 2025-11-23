using TempooERP.BuildingBlocks.Application.Persistence;
using TempooERP.Modules.Sales.Domain.Orders;

namespace TempooERP.Modules.Sales.Application.Abstractions;

public interface ISalesWriteDbContext : IWriteDbContext
{
    IQueryable<Order> Orders { get; }
    IQueryable<OrderLine> OrderLines { get; }
}
