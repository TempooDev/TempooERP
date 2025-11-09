namespace TempooERP.Modules.Catalog.Contracts.IntegrationEvents;

public sealed record ProductChanged(Guid Id, string Name, decimal Price, int TaxRate, bool Active, DateTime OccurredAtUtc);
