using Microsoft.AspNetCore.Mvc;
using TempooERP.BuildingBlocks.Application.Abstractions;
using TempooERP.BuildingBlocks.Application.Extensions;
using TempooERP.Modules.Sales.Application.Orders.Commands.CreateOrder;
using TempooERP.Modules.Sales.Application.Orders.Commands.UpdateOrder;
using TempooERP.Modules.Sales.Application.Orders.Queries.GetByCriteria;
using TempooERP.Modules.Sales.Application.Orders.Queries.GetOrderById;
using TempooERP.Modules.Sales.Contracts;

namespace TempooERP.Api.Modules;

public static class OrdersEndpoints
{
    extension(RouteGroupBuilder group)
    {
        public void MapOrdersEndpoints()
        {
            // Queries
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

            group.MapGet("/orders/{id}", async (
                Guid id,
                [FromServices] IQueryHandler<GetOrderByIdQuery, OrderDto?> queryHandler,
                CancellationToken ct) =>
            {
                var result = await queryHandler.HandleAsync(new GetOrderByIdQuery(id), ct);

                return result is null
                    ? Results.NotFound(Result.Fail($"Order with id {id} not found"))
                    : Results.Ok(Result.Ok(result));
            })
            .WithTags(SalesEndpoints.Tag)
            .WithName("GetOrderById")
            .WithSummary("Get the order by Id, if it doesn't exist returns 404");

            // Commands
            group.MapPost("/orders", async (
                [FromServices] ICommandHandler<CreateOrderCommand, Guid> commandHandler,
                [FromBody] CreateOrderCommand command,
                CancellationToken cancellationToken) =>
            {
                var orderId = await commandHandler.HandleAsync(command, cancellationToken);
                return Results.Created(
                    $"/api/sales/orders/{orderId}",
                    Result.Ok(orderId, "Order created successfully."));
            })
            .WithTags(SalesEndpoints.Tag)
            .WithName("CreateOrder")
            .WithSummary("Creates a new order with order lines.");

            group.MapPut("/orders/{id}", async (
                [FromRoute] Guid id,
                [FromBody] UpdateOrderDto updateDto,
                [FromServices] ICommandHandler<UpdateOrderCommand> commandHandler,
                CancellationToken cancellationToken
            ) =>
            {
                var command = new UpdateOrderCommand(id, updateDto.Status);
                await commandHandler.HandleAsync(command, cancellationToken);
                return Results.NoContent();
            })
            .WithTags(SalesEndpoints.Tag)
            .WithName("UpdateOrder")
            .WithSummary("Updates an order's status.");
        }
    }
}