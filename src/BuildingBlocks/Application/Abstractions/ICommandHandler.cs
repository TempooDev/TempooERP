namespace TempooERP.BuildingBlocks.Application.Abstractions;

public interface ICommandHandler<TCommand> where TCommand : ICommandEntity
{
    Task HandleAsync(TCommand command, CancellationToken cancellationToken);
}