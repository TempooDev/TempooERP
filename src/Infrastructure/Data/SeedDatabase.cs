using Microsoft.EntityFrameworkCore;
using TempooERP.Modules.Catalog.Domain.Products;
using TempooERP.Modules.Sales.Domain.Orders;

namespace TempooERP.Infrastructure.Data;

public sealed class SeedDatabase(ErpDbContext dbContext)
{
    private readonly ErpDbContext _dbContext = dbContext;

    public async Task SeedAsync()
    {
        if (!await _dbContext.Products.AnyAsync())
        {
            await SeedProductsAsync();
        }

        if (!await _dbContext.Orders.AnyAsync())
        {
            await SeedOrdersAsync();
        }

    }

    private async Task SeedProductsAsync()
    {
        var products = new List<Product>
        {
            Product.CreateProduct("Sample Product 1", 9.99M, 21, true),
            Product.CreateProduct("Sample Product 2", 19.99M, 10, true),
            Product.CreateProduct("Sample Product 3", 29.99M, 21, false),
        };

        await _dbContext.Products.AddRangeAsync(products);
        await _dbContext.SaveChangesAsync();
    }

    private async Task SeedOrdersAsync()
    {
        var products = await _dbContext.Products.Take(3).ToListAsync();
        if (products.Count == 0)
        {
            return;
        }

        var order1 = Order.CreateOrder(1001, Guid.NewGuid());
        var order2 = Order.CreateOrder(1002, Guid.NewGuid());

        // Order 1 lines
        var p1 = products[0];
        order1.AddLine(OrderLine.CreateOrderLine(order1.Id, p1.Id, p1.Name, p1.Price, 2));

        if (products.Count > 1)
        {
            var p2 = products[1];
            order1.AddLine(OrderLine.CreateOrderLine(order1.Id, p2.Id, p2.Name, p2.Price, 1));
        }

        // Order 2 lines
        var p3 = products[0];
        order2.AddLine(OrderLine.CreateOrderLine(order2.Id, p3.Id, p3.Name, p3.Price, 5));

        if (products.Count > 2)
        {
            var p4 = products[2];
            order2.AddLine(OrderLine.CreateOrderLine(order2.Id, p4.Id, p4.Name, p4.Price, 3));
        }

        await _dbContext.Orders.AddRangeAsync([order1, order2]);
        await _dbContext.SaveChangesAsync();
    }
}
