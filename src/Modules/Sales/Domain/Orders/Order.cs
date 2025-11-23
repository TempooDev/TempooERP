using TempooERP.BuildingBlocks.Domain;

namespace TempooERP.Modules.Sales.Domain.Orders;

public sealed class Order : AuditableEntity
{
    private readonly List<OrderLine> _orderLines = [];

    public long Number { get; private set; }
    public OrderStatus Status { get; private set; } = OrderStatus.Pending;
    public Guid UserId { get; private set; }

    public IReadOnlyCollection<OrderLine> OrderLines => _orderLines;

    public Order() { } // EF

    private Order(long number, Guid userId, OrderStatus status)
    {
        Number = number;
        UserId = userId;
        Status = status;
    }

    public static Order CreateOrder(long number, Guid userId, OrderStatus status = OrderStatus.Pending)
        => new(number, userId, status);

    public void AddLine(OrderLine line) => _orderLines.Add(line);

    public void UpdateStatus(OrderStatus status) => Status = status;
}
