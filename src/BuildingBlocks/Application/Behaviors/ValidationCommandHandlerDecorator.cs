using FluentValidation;
using TempooERP.BuildingBlocks.Application.Abstractions;

namespace TempooERP.BuildingBlocks.Application.Behaviors;

public sealed class ValidationCommandHandlerDecorator<TCommand>(
    ICommandHandler<TCommand> inner,
    IValidator<TCommand>? validator = null) : ICommandHandler<TCommand>
    where TCommand : class, ICommandEntity
{
    private readonly ICommandHandler<TCommand> _inner = inner;
    private readonly IValidator<TCommand>? _validator = validator;

    public async Task HandleAsync(TCommand command, CancellationToken cancellationToken = default)
    {
        if (_validator is not null)
        {
            var result = await _validator.ValidateAsync(command, cancellationToken);

            if (!result.IsValid)
            {
                var errors = result.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => char.ToLowerInvariant(g.Key[0]) + g.Key[1..],
                        g => g.Select(e => e.ErrorMessage).ToArray()
                    );
                throw new ValidationException("Validation failed", result.Errors);
            }
        }

        await _inner.HandleAsync(command, cancellationToken);
    }
}

public sealed class ValidationCommandHandlerDecorator<TCommand, TResponse>(
    ICommandHandler<TCommand, TResponse> inner,
    IValidator<TCommand> validator)
    : ICommandHandler<TCommand, TResponse>
    where TCommand : class, ICommandEntity
{
    private readonly ICommandHandler<TCommand, TResponse> _inner = inner;
    private readonly IValidator<TCommand> _validator = validator;

    public async Task<TResponse> HandleAsync(TCommand command, CancellationToken ct = default)
    {
        var result = await _validator.ValidateAsync(command, ct);

        return !result.IsValid
            ? throw new ValidationException("Validation failed", result.Errors)
            : await _inner.HandleAsync(command, ct);
    }
}