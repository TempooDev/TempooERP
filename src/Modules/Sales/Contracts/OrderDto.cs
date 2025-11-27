namespace TempooERP.Modules.Sales.Contracts;

public sealed record OrderDto(
    Guid Id,
    long Number,
    string Status,
    Guid UserId,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    IReadOnlyCollection<OrderLineDto> OrderLines);
