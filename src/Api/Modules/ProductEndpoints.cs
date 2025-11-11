using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TempooERP.Api.Modules;
using TempooERP.BuildingBlocks.Application.Abstractions;
using TempooERP.BuildingBlocks.Application.Extensions;
using TempooERP.Modules.Catalog.Application.Products.Commands.CreateProduct;
using TempooERP.Modules.Catalog.Application.Products.Commands.DeleteProduct;
using TempooERP.Modules.Catalog.Application.Products.Commands.UpdateProduct;
using TempooERP.Modules.Catalog.Application.Products.Queries.GetByCriteria;

namespace TempooERP.Api.Modules;

public static class ProductEndpoints
{
    extension(RouteGroupBuilder group)
    {
        public void MapProductEndpoints()
        {
            // Queries
            group.MapGet("/products", async (
                [AsParameters] GetProductByCriteriaQuery query,
                IQueryHandler<GetProductByCriteriaQuery, PagedResult<ProductDto>> queryHandler,
                CancellationToken cancellationToken) =>
            {
                var result = await queryHandler.HandleAsync(query, cancellationToken);
                return Results.Ok(result.ToResult());
            })
            .WithTags(CatalogEndpoints.Tag)
            .WithName("GetProducts")
            .WithSummary("Gets the paged list of products with optional filters.");

            // Commands
            group.MapPost("/products", async (
                [FromServices] ICommandHandler<CreateProductCommand, Guid> commandHandler,
                [FromBody] CreateProductCommand command,
                CancellationToken cancellationToken) =>
            {
                var productId = await commandHandler.HandleAsync(command, cancellationToken);
                return Results.Created(
                    $"/api/catalog/products/{productId}",
                    Result.Ok("Product created successfully."));
            })
            .WithTags(CatalogEndpoints.Tag)
            .WithName("CreateProduct")
            .WithSummary("Creates a new product.");

            group.MapPut("/products/{id}", async (
                [FromRoute] Guid id,
                [FromBody] UpdateProductCommand command,
                [FromServices] ICommandHandler<UpdateProductCommand> commandHandler,
                CancellationToken cancellationToken
            ) =>
            {
                await commandHandler.HandleAsync(command, cancellationToken);
                return Results.NoContent();
            })
            .WithTags(CatalogEndpoints.Tag)
            .WithName("UpdateProduct")
            .WithSummary("Updates an existing product.");

            group.MapDelete("/products/{id}", async (
                [FromRoute] Guid id,
                [FromServices] ICommandHandler<DeleteProductCommand> commandHandler,
                CancellationToken cancellationToken) =>
            {
                var command = new DeleteProductCommand(id);
                await commandHandler.HandleAsync(command, cancellationToken);
                return Results.NoContent();
            })
            .WithTags(CatalogEndpoints.Tag)
            .WithName("DeleteProduct")
            .WithSummary("Deletes a product by ID.");
        }
    }
}