using TempooERP.Modules.Catalog.Domain.Products;

namespace TempooERP.Modules.Catalog.Application.Abstractions;

/// <summary>
/// Read-only abstraction over the ERP DbContext for Catalog queries.
/// </summary>
public interface ICatalogReadDbContext
{
    IQueryable<Product> Products { get; }
}
