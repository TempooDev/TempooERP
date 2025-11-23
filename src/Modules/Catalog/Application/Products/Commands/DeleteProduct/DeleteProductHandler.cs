using TempooERP.BuildingBlocks.Application.Abstractions;
using TempooERP.BuildingBlocks.Application.Persistence;
using TempooERP.Modules.Catalog.Application.Abstractions;

namespace TempooERP.Modules.Catalog.Application.Products.Commands.DeleteProduct;

public sealed class DeleteProductHandler(
    IUnitOfWork unitOfWork,
    ICatalogWriteDbContext dbContext) : ICommandHandler<DeleteProductCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICatalogWriteDbContext _dbContext = dbContext;

    public async Task HandleAsync(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var product = _dbContext.Products.FirstOrDefault(p => p.Id == command.ProductId)
            ?? throw new InvalidOperationException($"Product with ID {command.ProductId} not found.");

        _dbContext.Delete(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken); // Commit transaction
    }
}