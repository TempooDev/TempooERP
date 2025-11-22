using Microsoft.Extensions.DependencyInjection;
using TempooERP.BuildingBlocks.Application.Abstractions;
using TempooERP.BuildingBlocks.Application.Extensions;
using TempooERP.Modules.Catalog.Application.Products.Commands.CreateProduct;
using TempooERP.Modules.Catalog.Application.Products.Commands.DeleteProduct;
using TempooERP.Modules.Catalog.Application.Products.Commands.UpdateProduct;
using TempooERP.Modules.Catalog.Application.Products.Queries;
using TempooERP.Modules.Catalog.Application.Products.Queries.GetByCriteria;
using TempooERP.Modules.Catalog.Application.Products.Queries.GetById;

namespace TempooERP.Modules.Catalog.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureCatalogServices(this IServiceCollection services)
    {

        services.AddQueryHandler<
            GetProductByCriteriaQuery,
            PagedResult<ProductDto>,
            GetProductByCriteriaHandler>();

        services.AddQueryHandler<
            GetProductByIdQuery,
            Result<ProductDto?>,
            GetProductByIdHandler>();

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
