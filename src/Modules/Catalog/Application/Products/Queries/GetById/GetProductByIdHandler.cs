using TempooERP.BuildingBlocks.Application.Abstractions;
using TempooERP.Modules.Catalog.Application.Abstractions;

namespace TempooERP.Modules.Catalog.Application.Products.Queries.GetById;

public sealed class GetProductByIdHandler(IProductReadRepository products) : IQueryHandler<GetProductByIdQuery,Result<ProductDto?>>
{
    private readonly IProductReadRepository _productsRepository = products;

    public async Task<Result<ProductDto?>> HandleAsync(
        GetProductByIdQuery query,
        CancellationToken cancellationToken)
    {
        var product =  await _productsRepository.GetByIdAsync(query.Id, cancellationToken);

        return Result<ProductDto?>.Ok(product);
    }
}

