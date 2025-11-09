using TempooERP.Modules.Catalog.Domain.Products;

namespace TempooERP.Modules.Catalog.Application.Abstractions;

/// <summary>
/// Read-only abstraction over the ERP DbContext for Catalog queries.
/// </summary>
public interface IErpWriteDbContext
{
    IQueryable<Product> Products { get; }

    Task AddAsync<T>(T entity, CancellationToken cancellationToken) where T : class;

    void Update<T>(T entity) where T : class;

    void Delete<T>(T entity) where T : class;
}
