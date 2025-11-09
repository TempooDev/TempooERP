using TempooERP.BuildingBlocks.Application;
using TempooERP.Modules.Catalog.Application.Abstractions;

namespace TempooERP.Modules.Catalog.Application.Products.Queries.GetProductsList;

public sealed class GetProductsListHandler(IProductReadRepository products) : IQueryHandler<GetProductsListQuery, IEnumerable<ProductListDto>>
{
    private readonly IProductReadRepository _products = products;

    public async Task<IEnumerable<ProductListDto>> HandleAsync(GetProductsListQuery query, CancellationToken cancellationToken)
    {
        var items = await _products.GetAllAsync(cancellationToken);
        return items;
    }
}