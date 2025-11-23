using TempooERP.BuildingBlocks.Application.Abstractions;
using TempooERP.BuildingBlocks.Application.Persistence;
using TempooERP.Modules.Catalog.Application.Abstractions;
using TempooERP.Modules.Catalog.Domain.Products;

namespace TempooERP.Modules.Catalog.Application.Products.Commands.UpdateProduct;

public sealed class UpdateProductHandler(
    IUnitOfWork unitOfWork,
    ICatalogWriteDbContext dbContext) : ICommandHandler<UpdateProductCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICatalogWriteDbContext _dbContext = dbContext;

    public async Task HandleAsync(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = _dbContext.Products.FirstOrDefault(p => p.Id == command.Id)
            ?? throw new InvalidOperationException($"Product with ID {command.Id} not found.");

        product = Product.UpdateDetails(
            product,
            command.Name,
            command.Price,
            command.IsActive);

        _dbContext.Update(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken); // Commit transaction
    }
}