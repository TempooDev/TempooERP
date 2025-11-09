using Microsoft.EntityFrameworkCore;
using TempooERP.Modules.Catalog.Domain.Products;

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
           Product.CreateProduct("Sample Product 1",9.99M,21,true),
            Product.CreateProduct("Sample Product 2", 19.99M, 10, true),
            Product.CreateProduct("Sample Product 3", 29.99M, 21, false),
        };

        await _dbContext.Products.AddRangeAsync(products);
        await _dbContext.SaveChangesAsync();
    }
}