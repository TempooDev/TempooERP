namespace TempooERP.Modules.Catalog.Contracts;

public sealed record ProductSummaryDto(Guid Id, string Name, decimal Price, int TaxRate, bool IsActive);
