using Microsoft.Extensions.DependencyInjection;
using TempooERP.BuildingBlocks.Application;
using TempooERP.Modules.Catalog.Application.Products.Queries.GetProductsList;

namespace TempooERP.Modules.Catalog.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureCatalogServices(this IServiceCollection services)
    {
        // Register the query handler for GetProductsList
        services.AddScoped<IQueryHandler<
            GetProductsListQuery,
            IEnumerable<ProductListDto>>,
            GetProductsListHandler>();

        return services;
    }
}
