using System.Linq.Expressions;
using TempooERP.BuildingBlocks.Application.Abstractions;
using TempooERP.Modules.Catalog.Application.Abstractions;
using TempooERP.Modules.Catalog.Domain.Products;

namespace TempooERP.Modules.Catalog.Application.Products.Queries.GetByCriteria;

public sealed class GetProductByCriteriaHandler(IProductReadRepository products)
: IQueryHandler<GetProductByCriteriaQuery, PagedResult<ProductDto>>
{
    private readonly IProductReadRepository _products = products;

    public async Task<PagedResult<ProductDto>> HandleAsync(
        GetProductByCriteriaQuery query,
        CancellationToken cancellationToken)
    {
        var predicate = BuildPredicate(query);
        return await _products.SearchAsync(
            predicate,
            query.Page,
            query.PageSize,
            query.SortBy,
            query.SortDirection,
            cancellationToken);
    }

    public static Expression<Func<Product, bool>> BuildPredicate(GetProductByCriteriaQuery query)
    {
        return p =>
            (string.IsNullOrEmpty(query.Search) || p.Name.Contains(query.Search)) &&
            (string.IsNullOrEmpty(query.Name) || p.Name == query.Name) &&
            (!query.IsActive.HasValue || p.IsActive == query.IsActive.Value) &&
            (!query.TaxRateLower.HasValue || p.TaxRate >= query.TaxRateLower.Value) &&
            (!query.TaxRateUpper.HasValue || p.TaxRate <= query.TaxRateUpper.Value) &&
            (!query.PriceLower.HasValue || p.Price >= query.PriceLower.Value) &&
            (!query.PriceUpper.HasValue || p.Price <= query.PriceUpper.Value);
    }
}