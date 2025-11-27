using FluentValidation;
using TempooERP.BuildingBlocks.Application.Abstractions;

namespace TempooERP.Modules.Sales.Application.Orders.Commands.UpdateOrder;

public sealed record UpdateOrderCommand(
    Guid Id,
    string Status
) : ICommandEntity;

public sealed class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Order Id is required");

        RuleFor(x => x.Status)
            .NotEmpty()
            .WithMessage("Status is required")
            .Must(BeAValidStatus)
            .WithMessage("Invalid status. Valid values are: Pending, Processing, Completed, Canceled");
    }

    private static bool BeAValidStatus(string status)
    {
        return Enum.TryParse<Domain.Orders.OrderStatus>(status, true, out _);
    }
}
