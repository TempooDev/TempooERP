using System.ComponentModel.DataAnnotations;
using TempooERP.BuildingBlocks.Application.Abstractions;

namespace TempooERP.Modules.Catalog.Application.Products.Commands.UpdateProduct;

public sealed record UpdateProductCommand(
    [Required]
    Guid Id,
    [MaxLength(200), MinLength(3)]
    string? Name,
    [Range(0.01, 1000000)]
    decimal? Price,
    [Range(0, 100)]
    int? TaxRate,
    bool? IsActive
) : ICommandEntity;