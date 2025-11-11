using TempooERP.BuildingBlocks.Application;
using TempooERP.BuildingBlocks.Application.Abstractions;
using TempooERP.Modules.Catalog.Application.Abstractions;
using TempooERP.Modules.Catalog.Domain.Products;

namespace TempooERP.Modules.Catalog.Application.Products.Commands.CreateProduct;

public sealed class CreateProductHandler(
    IUnitOfWork unitOfWork,
    IErpWriteDbContext dbContext) : ICommandHandler<CreateProductCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IErpWriteDbContext _dbContext = dbContext;

    public async Task<Guid> HandleAsync(
        CreateProductCommand command,
        CancellationToken ct = default)
    {
        var product = Product.CreateProduct(
            command.Name,
            command.Price,
            command.TaxRate,
            command.IsActive);

        await _dbContext.AddAsync(product, ct);
        await _unitOfWork.SaveChangesAsync(ct);
        return product.Id;
    }
}