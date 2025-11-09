using Microsoft.Extensions.DependencyInjection;
using TempooERP.BuildingBlocks.Application;
using TempooERP.Modules.Catalog.Application.Products.Commands.CreateProduct;
using TempooERP.Modules.Catalog.Application.Products.Commands.DeleteProduct;
using TempooERP.Modules.Catalog.Application.Products.Commands.UpdateProduct;
using TempooERP.Modules.Catalog.Application.Products.Queries.GetProductsList;

namespace TempooERP.Modules.Catalog.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureCatalogServices(this IServiceCollection services)
    {
        services.AddScoped<IQueryHandler<
            GetProductsListQuery,
            IEnumerable<ProductListDto>>,
            GetProductsListHandler>();

        services.AddScoped<ICommandHandler<
            CreateProductCommand>,
            CreateProductHandler>();

        services.AddScoped<ICommandHandler<
            UpdateProductCommand>,
            UpdateProductHandler>();

        services.AddScoped<ICommandHandler<
            DeleteProductCommand>,
            DeleteProductHandler>();
        return services;
    }
}
