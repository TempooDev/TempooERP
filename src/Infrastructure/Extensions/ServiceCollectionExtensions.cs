using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using TempooERP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace TempooERP.Infrastructure.Extensions;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddInfrastructure(IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(configuration);

            var connection = configuration.GetConnectionString("erp-database");
            if (string.IsNullOrWhiteSpace(connection))
            {
                throw new InvalidOperationException(
                    "Connection string 'erp-database' not found in configuration (ConnectionStrings:erp-database).");
            }

            services.AddPooledDbContextFactory<ErpDbContext>(opts =>
            {
                opts.UseNpgsql(connection);
                opts.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            services.AddScoped(sp =>
            {
                var factory = sp.GetRequiredService<IDbContextFactory<ErpDbContext>>();
                return factory.CreateDbContext();
            });
            return services;
        }
    }
}