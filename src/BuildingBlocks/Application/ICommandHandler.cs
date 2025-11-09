namespace TempooERP.BuildingBlocks.Application;

public interface ICommandHandler<TCommand> where TCommand : ICommandEntity
{
    Task HandleAsync(TCommand command, CancellationToken cancellationToken);
}