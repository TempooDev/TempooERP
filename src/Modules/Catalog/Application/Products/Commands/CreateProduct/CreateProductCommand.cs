using FluentValidation;
using TempooERP.BuildingBlocks.Application.Abstractions;

namespace TempooERP.Modules.Catalog.Application.Products.Commands.CreateProduct;

public sealed record CreateProductCommand(
    string Name,
    decimal Price,
    int TaxRate,
    bool IsActive
) : ICommandEntity;

public sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(200);

        RuleFor(x => x.Price)
            .NotEmpty()
            .InclusiveBetween(0.01m, 1_000_000m);

        RuleFor(x => x.TaxRate)
            .NotEmpty()
            .InclusiveBetween(0, 100);
    }
}
