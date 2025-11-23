using Microsoft.EntityFrameworkCore;
using TempooERP.BuildingBlocks.Domain;
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

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = "system"; // _currentUserService.UserId;'
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedBy = "system";
                    entry.Entity.LastModifiedAt = DateTime.UtcNow;
                    break;
                case EntityState.Detached:
                    break;
                case EntityState.Unchanged:
                    break;
                case EntityState.Deleted:
                    break;
                default:
                    break;
            }
        }

        var result = await base.SaveChangesAsync(cancellationToken);

        return result;
    }
}
