using Microsoft.EntityFrameworkCore;
using TempooERP.Modules.Catalog.Application.Abstractions;
using TempooERP.Modules.Catalog.Domain.Products;

namespace TempooERP.Infrastructure.Data.Modules.Catalog;

/// <summary>
/// Adapter exposing read-only IQueryable surfaces required by application layer.
/// </summary>
public sealed class CatalogReadDbContextAdapter(ErpDbContext db) : ICatalogReadDbContext
{
    public IQueryable<Product> Products => db.Products.AsNoTracking();
}
