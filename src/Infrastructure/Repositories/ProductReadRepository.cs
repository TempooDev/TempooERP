using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TempooERP.BuildingBlocks.Application.Abstractions;
using TempooERP.BuildingBlocks.Application.Extensions;
using TempooERP.Modules.Catalog.Application.Abstractions;
using TempooERP.Modules.Catalog.Application.Products.Queries;
using TempooERP.Modules.Catalog.Domain.Products;

namespace TempooERP.Infrastructure.Repositories;

public sealed class ProductReadRepository(ICatalogReadDbContext db) : IProductReadRepository
{
    private readonly ICatalogReadDbContext _db = db;

    public async Task<ProductDto?> GetByIdAsync(
        Guid id,
        CancellationToken ct = default)
    {
        var product = await _db
            .Products
            .AsNoTracking()
            .SingleOrDefaultAsync(p => p.Id == id, ct);

        return product is not null ?
            new ProductDto(
                product.Id,
                product.Name,
                product.Price,
                product.TaxRate,
                product.IsActive)
        : null;
    }

    public async Task<PagedResult<ProductDto>> SearchAsync(
        Expression<Func<Product, bool>> predicate,
        int page,
        int pageSize,
        string? sortBy,
        string? sortDirection,
        CancellationToken ct = default)
    {
        var query = _db.Products
                   .AsNoTracking()
                   .Where(predicate);

        query = query.ApplyOrdering(sortBy, sortDirection);

        var total = await query.CountAsync(ct);

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(p => new ProductDto(
                p.Id,
                p.Name,
                p.Price,
                p.TaxRate,
                p.IsActive
                ))
            .ToListAsync(ct);

        return new PagedResult<ProductDto>(items, total, page, pageSize);
    }
}
