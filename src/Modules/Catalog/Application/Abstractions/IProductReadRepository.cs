using System.Linq.Expressions;
using TempooERP.BuildingBlocks.Application.Abstractions;
using TempooERP.Modules.Catalog.Application.Products.Queries;
using TempooERP.Modules.Catalog.Domain.Products;

namespace TempooERP.Modules.Catalog.Application.Abstractions;

/// <summary>
/// Repository abstraction for read operations on Products.
/// </summary>
public interface IProductReadRepository
{
    Task<PagedResult<ProductDto>> SearchAsync(
        Expression<Func<Product, bool>> predicate,
        int page,
        int pageSize,
        string? sortBy,
        string? sortDirection,
        CancellationToken ct = default);

    Task<ProductDto?> GetByIdAsync(
        Guid id,
        CancellationToken ct= default);
}
