namespace TempooERP.Modules.Sales.Contracts;

public sealed record OrderLineDto(
    Guid Id,
    Guid ProductId,
    string ProductName,
    decimal UnitPrice,
    int Quantity,
    decimal TotalLinePrice);
