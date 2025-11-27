namespace TempooERP.Modules.Sales.Application.Orders;

public sealed record OrderDto(
    Guid Id,
    Guid CustomerId,
    long Number,
    decimal TotalAmount,
    string Status);