using TempooERP.BuildingBlocks.Application.Abstractions;
using TempooERP.Modules.Sales.Application.Abstractions;
using TempooERP.Modules.Sales.Contracts;

namespace TempooERP.Modules.Sales.Application.Orders.Queries.GetOrderById;

public sealed class GetOrderByIdHandler(IOrderReadRepository orders)
    : IQueryHandler<GetOrderByIdQuery, OrderDto?>
{
    private readonly IOrderReadRepository _orders = orders;

    public async Task<OrderDto?> HandleAsync(
        GetOrderByIdQuery query,
        CancellationToken cancellationToken)
    {
        return await _orders.GetByIdAsync(query.Id, cancellationToken);
    }
}
