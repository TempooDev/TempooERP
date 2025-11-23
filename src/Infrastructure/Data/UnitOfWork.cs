using TempooERP.BuildingBlocks.Application.Persistence;

namespace TempooERP.Infrastructure.Data;

/// <summary>
/// Unit of Work implementation bridging application layer commits to EF Core context.
/// </summary>
public sealed class UnitOfWork(ErpDbContext db) : IUnitOfWork
{
    private readonly ErpDbContext _db = db;

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => _db.SaveChangesAsync(cancellationToken);
}
