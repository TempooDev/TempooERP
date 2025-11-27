namespace TempooERP.Modules.Sales.Application.Orders;

public record OrderLineDto(
    Guid Id,
    Guid ProductId,
    string ProductName,
    int Quantity,
    decimal UnitPrice);