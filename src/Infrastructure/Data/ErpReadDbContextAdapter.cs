using Microsoft.EntityFrameworkCore;
using TempooERP.Modules.Catalog.Application.Abstractions;
using TempooERP.Modules.Catalog.Domain;

namespace TempooERP.Infrastructure.Data;

/// <summary>
/// Adapter exposing read-only IQueryable surfaces required by application layer.
/// </summary>
public sealed class ErpReadDbContextAdapter(ErpDbContext db) : IErpReadDbContext
{
    private readonly ErpDbContext _db = db;

    public IQueryable<Product> Products => _db.Products.AsNoTracking();
}
