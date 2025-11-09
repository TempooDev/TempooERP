namespace TempooERP.BuildingBlocks.Application;

public interface IQueryHandler<TQuery, TResult> where TQuery : IQueryEntity
{
    Task<TResult> HandleAsync(TQuery query, CancellationToken cancellationToken);
}