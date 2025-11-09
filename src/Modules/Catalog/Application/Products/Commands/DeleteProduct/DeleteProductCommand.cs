using TempooERP.BuildingBlocks.Application;

namespace TempooERP.Modules.Catalog.Application.Products.Commands.DeleteProduct;

public sealed record DeleteProductCommand(
   Guid ProductId
) : ICommandEntity;