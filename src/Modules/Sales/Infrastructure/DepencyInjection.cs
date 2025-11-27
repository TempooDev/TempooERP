using Microsoft.Extensions.DependencyInjection;
using TempooERP.BuildingBlocks.Application.Abstractions;
using TempooERP.BuildingBlocks.Application.Extensions;
using TempooERP.Modules.Sales.Application.Orders.Commands.CreateOrder;
using TempooERP.Modules.Sales.Application.Orders.Commands.UpdateOrder;
using TempooERP.Modules.Sales.Application.Orders.Queries.GetByCriteria;
using TempooERP.Modules.Sales.Application.Orders.Queries.GetOrderById;
using TempooERP.Modules.Sales.Contracts;

namespace TempooERP.Modules.Sales.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureSalesServices(this IServiceCollection services)
    {
        // Query handlers
        services.AddQueryHandler<
            GetOrderByCriteriaQuery,
            PagedResult<OrderDto>,
            GetOrderByCriteriaHandler>();

        services.AddQueryHandler<
            GetOrderByIdQuery,
            OrderDto?,
            GetOrderByIdHandler>();

        // Command handlers
        services.AddCommandHandler<
            CreateOrderCommand,
            Guid,
            CreateOrderHandler>();

        services.AddCommandHandler<
            UpdateOrderCommand,
            UpdateOrderHandler>();

        return services;
    }
}
