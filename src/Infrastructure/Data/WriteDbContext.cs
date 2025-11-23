using TempooERP.BuildingBlocks.Application.Persistence;

namespace TempooERP.Infrastructure.Data;

public class WriteDbContext(ErpDbContext db) : IWriteDbContext
{
    private readonly ErpDbContext _db = db;
    public async Task AddAsync<T>(T entity, CancellationToken cancellationToken) where T : class => await _db.AddAsync(entity, cancellationToken);

    public void Update<T>(T entity) where T : class => _db.Update(entity);

    public void Delete<T>(T entity) where T : class => _db.Remove(entity);
}
