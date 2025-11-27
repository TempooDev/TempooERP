using Microsoft.EntityFrameworkCore;
using TempooERP.BuildingBlocks.Application.Abstractions;
using TempooERP.BuildingBlocks.Application.Persistence;
using TempooERP.Modules.Sales.Application.Abstractions;
using TempooERP.Modules.Sales.Domain.Orders;

namespace TempooERP.Modules.Sales.Application.Orders.Commands.UpdateOrder;

public sealed class UpdateOrderHandler(
    IUnitOfWork unitOfWork,
    ISalesWriteDbContext dbContext) : ICommandHandler<UpdateOrderCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ISalesWriteDbContext _dbContext = dbContext;

    public async Task HandleAsync(
        UpdateOrderCommand command,
        CancellationToken ct = default)
    {
        var order = await _dbContext.Orders
            .FirstOrDefaultAsync(o => o.Id == command.Id, ct)
            ?? throw new InvalidOperationException($"Order with Id {command.Id} not found");

        if (!Enum.TryParse<OrderStatus>(command.Status, true, out var status))
        {
            throw new ArgumentException($"Invalid status: {command.Status}");
        }

        order.UpdateStatus(status);
        _dbContext.Update(order);
        await _unitOfWork.SaveChangesAsync(ct);
    }
}
