namespace TempooERP.BuildingBlocks.Application;

public interface IQueryHandler<TQuery, TResult> where TQuery : IQueryEntity
{
    Task<TResult> Handle(TQuery query, CancellationToken cancellationToken);
}