
using TempooERP.BuildingBlocks.Application.Abstractions;
using TempooERP.BuildingBlocks.Application.Extensions;
using TempooERP.Modules.Sales.Application.Orders;
using TempooERP.Modules.Sales.Application.Orders.Queries.GetByCriteria;

namespace TempooERP.Api.Modules;

public static class OrdersEndpoints
{
    extension(RouteGroupBuilder group)
    {
        public void MapOrdersEndpoints()
        {
            group.MapGet("/orders", async (
                [AsParameters] GetOrderByCriteriaQuery query,
                IQueryHandler<GetOrderByCriteriaQuery, PagedResult<OrderDto>> queryHandler,
                CancellationToken cancellationToken) =>
            {
                var result = await queryHandler.HandleAsync(query, cancellationToken);
                return Results.Ok(result.ToResult());
            })
            .WithName("GetOrdersByCriteria")
            .WithTags(SalesEndpoints.Tag)
            .WithSummary("Gets the paged list of orders with optional filters.");
        }
    }
}