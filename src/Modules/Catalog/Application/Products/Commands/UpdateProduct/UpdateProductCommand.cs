using TempooERP.BuildingBlocks.Application;

namespace TempooERP.Modules.Catalog.Application.Products.Commands.UpdateProduct;

public sealed record UpdateProductCommand(
    Guid Id,
    string? Name,
    decimal? Price,
    bool? IsActive
) : ICommandEntity;