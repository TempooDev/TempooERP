using TempooERP.Api.Modules;
using TempooERP.BuildingBlocks.Application;
using TempooERP.Modules.Catalog.Application.Products.Queries.GetProductsList;

namespace TempooERP.Api.Modules;

public static class CatalogEndpoints
{
    private const string BasePath = "/api/catalog";
    private const string Tag = "Catalog";

    extension(IEndpointRouteBuilder endpoints)
    {
        public void MapCatalogEndpoints()
        {
            var catalog = endpoints.MapGroup(BasePath);

            catalog.MapGet("/products", async (
                IQueryHandler<GetProductsListQuery, IEnumerable<ProductListDto>> handler,
                CancellationToken ct) =>
            {
                var products = await handler.Handle(new GetProductsListQuery(), ct);
                return Results.Ok(products);
            })
            .WithName("GetProductsList")
            .WithTags(Tag);
        }
    }
}
