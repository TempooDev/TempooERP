using Microsoft.Extensions.DependencyInjection;
using TempooERP.BuildingBlocks.Application.Abstractions;
using TempooERP.BuildingBlocks.Application.Extensions;
using TempooERP.Modules.Catalog.Application.Products.Commands.CreateProduct;
using TempooERP.Modules.Catalog.Application.Products.Commands.DeleteProduct;
using TempooERP.Modules.Catalog.Application.Products.Commands.UpdateProduct;
using TempooERP.Modules.Catalog.Application.Products.Queries.GetByCriteria;

namespace TempooERP.Modules.Catalog.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureCatalogServices(this IServiceCollection services)
    {

        services.AddQueryHandler<
            GetProductByCriteriaQuery,
            PagedResult<ProductDto>,
            GetProductByCriteriaHandler>();

        services.AddValidatedCommandHandler<
            CreateProductCommand,
            Guid,
            CreateProductHandler,
            CreateProductCommandValidator>();

        services.AddCommandHandler<
            UpdateProductCommand,
            UpdateProductHandler>();

        services.AddCommandHandler<
            DeleteProductCommand,
            DeleteProductHandler>();

        return services;
    }
}
