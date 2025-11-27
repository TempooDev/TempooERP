namespace TempooERP.Api.Modules;

public static class SalesEndpoints
{
    private const string BasePath = "/api/sales";
    internal const string Tag = "Sales";

    extension(IEndpointRouteBuilder endpoints)
    {
        public void MapSalesEndpoints()
        {
            var sales = endpoints.MapGroup(BasePath);
            sales.MapOrdersEndpoints();
        }
    }
}
