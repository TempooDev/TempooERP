using Microsoft.EntityFrameworkCore;
using TempooERP.Modules.Catalog.Domain.Products;
using TempooERP.Modules.Sales.Domain.Orders;

namespace TempooERP.Infrastructure.Data;

public class ErpDbContext(DbContextOptions<ErpDbContext> options,
                    IEnumerable<IModuleModelBuilder> moduleBuilders) : DbContext(options)
{

    // Add DbSet<T> properties here as your domain entities are created.
    // Example:
    public DbSet<Product> Products { get; set; }

    public DbSet<Order> Orders { get; set; }

    public DbSet<OrderLine> OrderLines { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var builder in moduleBuilders)
        {
            builder.Configure(modelBuilder);
        }
    }
}
