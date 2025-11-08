using Microsoft.EntityFrameworkCore;
using TempooERP.Modules.Catalog.Domain;

namespace TempooERP.Infrastructure.Data;

public class ErpDbContext(DbContextOptions<ErpDbContext> options) : DbContext(options)
{

    // Add DbSet<T> properties here as your domain entities are created.
    // Example:
    public DbSet<Product> Products { get; set; }
}
