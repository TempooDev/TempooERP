using Microsoft.EntityFrameworkCore;

namespace TempooERP.Infrastructure.Data;

public class ErpDbContext(DbContextOptions<ErpDbContext> options) : DbContext(options)
{

    // Add DbSet<T> properties here as your domain entities are created.
    // Example:
    // public DbSet<Customer> Customers { get; set; }
}
