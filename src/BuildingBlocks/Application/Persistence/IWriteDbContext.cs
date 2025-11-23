namespace TempooERP.BuildingBlocks.Application.Persistence;

public interface IWriteDbContext
{

    Task AddAsync<T>(T entity, CancellationToken cancellationToken) where T : class;

    void Update<T>(T entity) where T : class;

    void Delete<T>(T entity) where T : class;
}
