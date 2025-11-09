
using TempooERP.Modules.Catalog.Application.Abstractions;
using TempooERP.Modules.Catalog.Domain.Products;

namespace TempooERP.Infrastructure.Data;

/// <summary>
/// Adapter exposing read-only IQueryable surfaces required by application layer.
/// </summary>
public sealed class ErpWriteDbContextAdapter(ErpDbContext db) : IErpWriteDbContext
{
    private readonly ErpDbContext _db = db;

    public IQueryable<Product> Products => _db.Products;

    public async Task AddAsync<T>(T entity, CancellationToken cancellationToken) where T : class => await _db.AddAsync(entity, cancellationToken);

    public void Update<T>(T entity) where T : class => _db.Update(entity);
    public void Delete<T>(T entity) where T : class => _db.Remove(entity);
}
