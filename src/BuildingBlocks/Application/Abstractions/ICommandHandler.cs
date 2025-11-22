namespace TempooERP.BuildingBlocks.Application.Abstractions;

public interface ICommandHandler<TCommand> where TCommand : ICommandEntity
{
    Task HandleAsync(TCommand command, CancellationToken cancellationToken);
}

public interface ICommandHandler<TCommand, TResponse> where TCommand : ICommandEntity
{
    Task<TResponse> HandleAsync(TCommand command, CancellationToken ct = default);
}
