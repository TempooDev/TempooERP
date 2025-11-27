using FluentValidation;
using TempooERP.BuildingBlocks.Application.Abstractions;

namespace TempooERP.Modules.Sales.Application.Orders.Commands.CreateOrder;

public sealed record CreateOrderLineCommand(
    Guid ProductId,
    string ProductName,
    decimal UnitPrice,
    int Quantity);

public sealed record CreateOrderCommand(
    Guid UserId,
    IReadOnlyCollection<CreateOrderLineCommand> OrderLines
) : ICommandEntity;

public sealed class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("UserId is required");

        RuleFor(x => x.OrderLines)
            .NotEmpty()
            .WithMessage("Order must have at least one line");

        RuleForEach(x => x.OrderLines)
            .ChildRules(line =>
            {
                line.RuleFor(l => l.ProductId)
                    .NotEmpty()
                    .WithMessage("ProductId is required");

                line.RuleFor(l => l.ProductName)
                    .NotEmpty()
                    .MaximumLength(200)
                    .WithMessage("ProductName is required and must be less than 200 characters");

                line.RuleFor(l => l.UnitPrice)
                    .GreaterThan(0)
                    .WithMessage("UnitPrice must be greater than 0");

                line.RuleFor(l => l.Quantity)
                    .GreaterThan(0)
                    .WithMessage("Quantity must be greater than 0");
            });
    }
}
