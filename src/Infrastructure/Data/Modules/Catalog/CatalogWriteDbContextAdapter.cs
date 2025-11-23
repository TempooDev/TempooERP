using TempooERP.Modules.Catalog.Application.Abstractions;
using TempooERP.Modules.Catalog.Domain.Products;

namespace TempooERP.Infrastructure.Data.Modules.Catalog;

/// <summary>
/// Adapter exposing read-only IQueryable surfaces required by application layer.
/// </summary>
public sealed class CatalogWriteDbContextAdapter(ErpDbContext db) : WriteDbContext(db), ICatalogWriteDbContext
{
    private readonly ErpDbContext _db = db;
    public IQueryable<Product> Products => _db.Products;
}
