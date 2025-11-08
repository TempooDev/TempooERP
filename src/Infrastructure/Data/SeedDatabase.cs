using Microsoft.EntityFrameworkCore;
using TempooERP.Infrastructure.Data;
using TempooERP.Modules.Catalog.Domain;

public class SeedDatabase
{
    private readonly IDbContextFactory<ErpDbContext> _dbContextFactory;

    public SeedDatabase(IDbContextFactory<ErpDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task SeedAsync()
    {
        await using var _dbContext = _dbContextFactory.CreateDbContext();
        if (await _dbContext.Products.AnyAsync())
        {
            return;
        }

        // Seed initial data
        var products = new List<Product>
        {
            new() { Name = "Sample Product 1",  Price = 9.99M, TaxRate= 21, IsActive=true },
            new() { Name = "Sample Product 2",  Price = 19.99M, TaxRate= 10, IsActive=true },
            new() { Name = "Sample Product 3",  Price = 29.99M, TaxRate= 21, IsActive=false },
        };

        await _dbContext.Products.AddRangeAsync(products);
        await _dbContext.SaveChangesAsync();
    }
}