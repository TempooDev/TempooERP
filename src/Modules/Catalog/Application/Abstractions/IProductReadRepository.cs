using TempooERP.Modules.Catalog.Application.Products.Queries.GetProductsList;

namespace TempooERP.Modules.Catalog.Application.Abstractions;

/// <summary>
/// Repository abstraction for read operations on Products.
/// </summary>
public interface IProductReadRepository
{
    Task<IEnumerable<ProductListDto>> GetAllAsync(CancellationToken cancellationToken);
}
