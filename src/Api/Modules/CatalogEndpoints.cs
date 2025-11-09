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

            catalog.MapProductEndpoints();
        }
    }

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
            .WithTags(Tag)
            .WithName("GetProducts")
            .WithSummary("Gets the list of products.");
        }
    }
}
