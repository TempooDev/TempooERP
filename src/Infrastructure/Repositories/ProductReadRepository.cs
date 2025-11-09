using Microsoft.EntityFrameworkCore;
using TempooERP.Modules.Catalog.Application.Abstractions;
using TempooERP.Modules.Catalog.Application.Products.Queries.GetProductsList;

namespace TempooERP.Infrastructure.Repositories;

public sealed class ProductReadRepository(IErpReadDbContext db) : IProductReadRepository
{
    private readonly IErpReadDbContext _db = db;

    public async Task<IEnumerable<ProductListDto>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        return await _db
            .Products
            .AsNoTracking()
            .Select(p => new ProductListDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
            })
            .OrderBy(p => p.Name)
            .ToListAsync(cancellationToken);
    }
}
