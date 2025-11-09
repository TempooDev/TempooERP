using Microsoft.AspNetCore.Mvc;
using TempooERP.Api.Modules;
using TempooERP.BuildingBlocks.Application;
using TempooERP.Modules.Catalog.Application.Products.Commands.CreateProduct;
using TempooERP.Modules.Catalog.Application.Products.Commands.DeleteProduct;
using TempooERP.Modules.Catalog.Application.Products.Commands.UpdateProduct;
using TempooERP.Modules.Catalog.Application.Products.Queries.GetProductsList;

namespace TempooERP.Api.Modules;

public static class ProductEndpoints
{
    extension(RouteGroupBuilder group)
    {
        public void MapProductEndpoints()
        {
            group.MapGet("/products", async (IQueryHandler<GetProductsListQuery, IEnumerable<ProductListDto>> queryHandler, CancellationToken cancellationToken) =>
            {
                var query = new GetProductsListQuery();
                var result = await queryHandler.HandleAsync(query, cancellationToken);
                return Results.Ok(result);
            })
            .WithTags(CatalogEndpoints.Tag)
            .WithName("GetProducts")
            .WithSummary("Gets the list of products.");

            group.MapPost("/products", async (
                [FromServices] ICommandHandler<CreateProductCommand> commandHandler,
                [FromBody] CreateProductCommand command,
                CancellationToken cancellationToken) =>
            {
                await commandHandler.HandleAsync(command, cancellationToken);
                return Results.Created($"/api/catalog/products/{command.Name}", null);
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