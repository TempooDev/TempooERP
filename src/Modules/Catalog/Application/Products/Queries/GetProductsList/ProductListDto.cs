namespace TempooERP.Modules.Catalog.Application.Products.Queries.GetProductsList;

public record ProductListDto
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public decimal Price { get; init; }
}