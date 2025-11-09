using TempooERP.BuildingBlocks.Application;
using TempooERP.Modules.Catalog.Application.Abstractions;
using TempooERP.Modules.Catalog.Domain.Products;

namespace TempooERP.Modules.Catalog.Application.Products.Commands.CreateProduct;

public sealed class CreateProductHandler(
    IUnitOfWork unitOfWork,
    IErpWriteDbContext dbContext) : ICommandHandler<CreateProductCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IErpWriteDbContext _dbContext = dbContext;

    public async Task HandleAsync(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = Product.CreateProduct(
            command.Name,
            command.Price,
            command.TaxRate,
            command.IsActive);

        await _dbContext.AddAsync(product, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken); // Commit transaction
    }
}