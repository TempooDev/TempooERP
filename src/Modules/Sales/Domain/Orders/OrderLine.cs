using TempooERP.BuildingBlocks.Domain;

namespace TempooERP.Modules.Sales.Domain.Orders;

public class OrderLine : EntityBase
{
    public Guid OrderId { get; private set; }

    // Navigation back to Order (optional but recommended)
    public Order Order { get; private set; } = null!;

    public Guid ProductId { get; private set; }
    public string ProductName { get; private set; } = string.Empty;
    public decimal UnitPrice { get; private set; }
    public int Quantity { get; private set; }

    public decimal TotalLinePrice => UnitPrice * Quantity;

    private OrderLine() { } // EF

    private OrderLine(
        Guid orderId,
        Guid productId,
        string productName,
        decimal unitPrice,
        int quantity)
    {
        OrderId = orderId;
        ProductId = productId;
        ProductName = productName;
        UnitPrice = unitPrice;
        Quantity = quantity;
    }

    public static OrderLine CreateOrderLine(
        Guid orderId,
        Guid productId,
        string productName,
        decimal unitPrice,
        int quantity)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
        if (string.IsNullOrWhiteSpace(productName))
        {
            throw new ArgumentException("Product name required.", nameof(productName));
        }

        ArgumentOutOfRangeException.ThrowIfNegative(unitPrice);
        return new(orderId, productId, productName, unitPrice, quantity);
    }

    public void UpdateQuantity(int quantity)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
        Quantity = quantity;
    }
}
