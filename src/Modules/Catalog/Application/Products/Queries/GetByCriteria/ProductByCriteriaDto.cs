namespace TempooERP.Modules.Catalog.Application.Products.Queries.GetByCriteria;

public record ProductDto(
    Guid Id,
    string Name,
    decimal Price,
    int TaxRate,
    bool IsActive);