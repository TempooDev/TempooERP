using TempooERP.Modules.Catalog.Domain;

namespace TempooERP.Modules.Catalog.Application.Abstractions;

/// <summary>
/// Read-only abstraction over the ERP DbContext for Catalog queries.
/// </summary>
public interface IErpReadDbContext
{
    IQueryable<Product> Products { get; }
}
