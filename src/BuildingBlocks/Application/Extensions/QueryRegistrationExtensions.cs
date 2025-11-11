using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TempooERP.BuildingBlocks.Application.Abstractions;
using TempooERP.BuildingBlocks.Application.Behaviors;

namespace TempooERP.BuildingBlocks.Application.Extensions;

public static class QueryRegistrationExtensions
{

    public static IServiceCollection AddQueryHandler<TQuery, TResponse, THandler>(
        this IServiceCollection services)
        where TQuery : class, IQueryEntity
        where THandler : class, IQueryHandler<TQuery, TResponse>
    {
        services.AddScoped<IQueryHandler<TQuery, TResponse>, THandler>();
        return services;
    }

    public static IServiceCollection AddValidatedQueryHandler<TQuery, TResponse, THandler, TValidator>(
        this IServiceCollection services)
        where TQuery : class, IQueryEntity
        where THandler : class, IQueryHandler<TQuery, TResponse>
        where TValidator : class, IValidator<TQuery>
    {
        // Handler
        services.AddScoped<THandler>();

        // Validator
        services.AddScoped<IValidator<TQuery>, TValidator>();

        // Decorador
        services.AddScoped<IQueryHandler<TQuery, TResponse>>(sp =>
        {
            var inner = sp.GetRequiredService<THandler>();
            var validator = sp.GetRequiredService<IValidator<TQuery>>();

            return new ValidationQueryHandlerDecorator<TQuery, TResponse>(inner, validator);
        });

        return services;
    }
}
