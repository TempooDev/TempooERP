using FluentValidation;
using TempooERP.BuildingBlocks.Application.Abstractions;

namespace TempooERP.BuildingBlocks.Application.Behaviors;

public sealed class ValidationQueryHandlerDecorator<TQuery, TResponse>(
    IQueryHandler<TQuery, TResponse> inner,
    IValidator<TQuery> validator)
    : IQueryHandler<TQuery, TResponse>
    where TQuery : class, IQueryEntity
{
    private readonly IQueryHandler<TQuery, TResponse> _inner = inner;
    private readonly IValidator<TQuery> _validator = validator;

    public async Task<TResponse> HandleAsync(TQuery query, CancellationToken cancellationToken = default)
    {
        var result = await _validator.ValidateAsync(query, cancellationToken);

        return !result.IsValid
            ? throw new ValidationException("Validation failed", result.Errors)
            : await _inner.HandleAsync(query, cancellationToken);
    }
}
