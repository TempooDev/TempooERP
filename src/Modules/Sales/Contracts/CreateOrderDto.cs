namespace TempooERP.Modules.Sales.Contracts;

public sealed record CreateOrderLineDto(
    Guid ProductId,
    string ProductName,
    decimal UnitPrice,
    int Quantity);

public sealed record CreateOrderDto(
    Guid UserId,
    IReadOnlyCollection<CreateOrderLineDto> OrderLines);
