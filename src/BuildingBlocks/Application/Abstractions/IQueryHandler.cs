namespace TempooERP.BuildingBlocks.Application.Abstractions;

public interface IQueryHandler<TQuery, TResult> where TQuery : IQueryEntity
{
    Task<TResult> HandleAsync(TQuery query, CancellationToken cancellationToken);
}