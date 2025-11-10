using TempooERP.BuildingBlocks.Application.Abstractions;

namespace TempooERP.Modules.Catalog.Application.Products.Commands.DeleteProduct;

public sealed record DeleteProductCommand(
   Guid ProductId
) : ICommandEntity;