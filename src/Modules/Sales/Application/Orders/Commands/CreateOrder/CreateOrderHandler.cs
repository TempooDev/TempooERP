using Microsoft.EntityFrameworkCore;
using TempooERP.BuildingBlocks.Application.Abstractions;
using TempooERP.BuildingBlocks.Application.Persistence;
using TempooERP.Modules.Sales.Application.Abstractions;
using TempooERP.Modules.Sales.Domain.Orders;

namespace TempooERP.Modules.Sales.Application.Orders.Commands.CreateOrder;

public sealed class CreateOrderHandler(
    IUnitOfWork unitOfWork,
    ISalesWriteDbContext dbContext) : ICommandHandler<CreateOrderCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ISalesWriteDbContext _dbContext = dbContext;

    public async Task<Guid> HandleAsync(
        CreateOrderCommand command,
        CancellationToken ct = default)
    {
        // Generate order number (simple implementation - could be improved with a sequence)
        var lastOrderNumber = await _dbContext.Orders
            .OrderByDescending(o => o.Number)
            .Select(o => o.Number)
            .FirstOrDefaultAsync(ct) ?? 0;

        var orderNumber = lastOrderNumber + 1;

        var order = Order.CreateOrder(orderNumber, command.UserId, OrderStatus.Pending);

        // Add order lines before saving
        foreach (var lineCommand in command.OrderLines)
        {
            var orderLine = OrderLine.CreateOrderLine(
                order.Id,
                lineCommand.ProductId,
                lineCommand.ProductName,
                lineCommand.UnitPrice,
                lineCommand.Quantity);

            order.AddLine(orderLine);
        }

        await _dbContext.AddAsync(order, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        return order.Id;
    }
}
