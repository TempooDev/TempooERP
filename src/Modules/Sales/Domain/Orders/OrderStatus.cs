namespace TempooERP.Modules.Sales.Domain.Orders;

public enum OrderStatus
{
    Pending = 0,
    Processing = 1,
    Completed = 2,
    Canceled = 3
}

public static class OrderStatusExtensions
{
    public static bool TryParse(string statusString, out OrderStatus status) => Enum.TryParse(statusString, true, out status);
}