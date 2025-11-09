using TempooERP.BuildingBlocks.Application;

namespace TempooERP.Modules.Catalog.Application.Products.Commands.CreateProduct;

public sealed record CreateProductCommand(
    string Name,
    decimal Price,
    int TaxRate,
    bool IsActive
) : ICommandEntity;