using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TempooERP.Infrastructure.Data;
using TempooERP.Modules.Catalog.Application.Abstractions;
using TempooERP.Infrastructure.Repositories;
using TempooERP.BuildingBlocks.Application;

namespace TempooERP.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
            ArgumentNullException.ThrowIfNull(configuration);

            var connection = configuration.GetConnectionString("erp-database");
            if (string.IsNullOrWhiteSpace(connection))
            {
                throw new InvalidOperationException(
                    "Connection string 'erp-database' not found in configuration (ConnectionStrings:erp-database).");
            }

        services.AddDbContext<ErpDbContext>(opts =>
        {
            opts.UseNpgsql(connection);
            opts.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });

    // Register adapters for application abstractions
    services.AddScoped<IErpReadDbContext, ErpReadDbContextAdapter>();
    services.AddScoped<IProductReadRepository, ProductReadRepository>();
    // Unit of Work
    services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}