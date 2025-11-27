using Microsoft.Extensions.DependencyInjection;
using TempooERP.BuildingBlocks.Application.Abstractions;
using TempooERP.BuildingBlocks.Application.Extensions;
using TempooERP.Modules.Sales.Application.Orders;
using TempooERP.Modules.Sales.Application.Orders.Queries.GetByCriteria;

namespace TempooERP.Modules.Sales.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureSalesServices(this IServiceCollection services)
    {
        services.AddQueryHandler<
            GetOrderByCriteriaQuery,
            PagedResult<OrderDto>,
            GetOrderByCriteriaHandler>();

        return services;
    }
}
