using Microsoft.EntityFrameworkCore;
using TempooERP.Modules.Catalog.Domain;

namespace TempooERP.Infrastructure.Data;

public sealed class SeedDatabase(ErpDbContext dbContext)
{
    private readonly ErpDbContext _dbContext = dbContext;

    public async Task SeedAsync()
    {
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