using TempooERP.BuildingBlocks.Application.Abstractions;

namespace TempooERP.Modules.Catalog.Application.Products.Queries.GetByCriteria;

public sealed record GetProductByCriteriaQuery(
    string? Search,
    string? Name,
    bool? IsActive,
    int? TaxRateLower,
    int? TaxRateUpper,
    decimal? PriceLower,
    decimal? PriceUpper,
    int Page,
    int PageSize,
    string? SortBy,
    string? SortDirection) : IQueryPagedEntity, IQueryEntity;