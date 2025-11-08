namespace TempooERP.Modules.Catalog.Contracts;

public interface IProductsQuery
{
    Task<ProductSummaryDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ProductSummaryDto>> GetAllAsync(CancellationToken cancellationToken = default);
}
