using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TempooERP.BuildingBlocks.Application.Abstractions;
using TempooERP.BuildingBlocks.Application.Behaviors;

namespace TempooERP.BuildingBlocks.Application.Extensions;

public static class CommandRegistrationExtensions
{
    public static IServiceCollection AddCommandHandler<TCommand, THandler>(
         this IServiceCollection services)
         where TCommand : class, ICommandEntity
         where THandler : class, ICommandHandler<TCommand>
    {
        services.AddScoped<ICommandHandler<TCommand>, THandler>();
        return services;
    }

    public static IServiceCollection AddCommandHandler<TCommand, TResponse, THandler>(
        this IServiceCollection services)
        where TCommand : class, ICommandEntity
        where THandler : class, ICommandHandler<TCommand, TResponse>
    {
        services.AddScoped<ICommandHandler<TCommand, TResponse>, THandler>();
        return services;
    }

    public static IServiceCollection AddValidatedCommandHandler<TCommand, THandler, TValidator>(
        this IServiceCollection services)
        where TCommand : class, ICommandEntity
        where THandler : class, ICommandHandler<TCommand>
        where TValidator : class, IValidator<TCommand>
    {
        // Handler as concrete service
        services.AddScoped<THandler>();

        // Validator
        services.AddScoped<IValidator<TCommand>, TValidator>();

        // Decorador like implementation of ICommandHandler<TCommand>
        services.AddScoped<ICommandHandler<TCommand>>(sp =>
        {
            var inner = sp.GetRequiredService<THandler>();
            var validator = sp.GetRequiredService<IValidator<TCommand>>();

            return new ValidationCommandHandlerDecorator<TCommand>(inner, validator);
        });

        return services;
    }

    public static IServiceCollection AddValidatedCommandHandler<TCommand, TResponse, THandler, TValidator>(
       this IServiceCollection services)
       where TCommand : class, ICommandEntity
       where THandler : class, ICommandHandler<TCommand, TResponse>
       where TValidator : class, IValidator<TCommand>
    {
        services.AddScoped<THandler>();
        services.AddScoped<IValidator<TCommand>, TValidator>();

        services.AddScoped<ICommandHandler<TCommand, TResponse>>(sp =>
        {
            var inner = sp.GetRequiredService<THandler>();
            var validator = sp.GetRequiredService<IValidator<TCommand>>();

            return new ValidationCommandHandlerDecorator<TCommand, TResponse>(inner, validator);
        });

        return services;
    }
}
