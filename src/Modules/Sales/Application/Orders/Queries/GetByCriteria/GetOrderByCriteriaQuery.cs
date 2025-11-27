using TempooERP.BuildingBlocks.Application.Abstractions;

namespace TempooERP.Modules.Sales.Application.Orders.Queries.GetByCriteria;

public sealed record GetOrderByCriteriaQuery(
    Guid? CustomerId,
    decimal? TotalAmountLower,
    decimal? TotalAmountUpper,
    string? Status,
    int Page,
    int PageSize,
    string? SortBy,
    string? SortDirection) : IQueryPagedEntity, IQueryEntity;