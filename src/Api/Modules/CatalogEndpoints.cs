namespace TempooERP.Api.Modules;

public static class CatalogEndpoints
{
    private const string BasePath = "/api/catalog";
    internal const string Tag = "Catalog";

    extension(IEndpointRouteBuilder endpoints)
    {
        public void MapCatalogEndpoints()
        {
            var catalog = endpoints.MapGroup(BasePath);

            catalog.MapProductEndpoints();
        }
    }
}
