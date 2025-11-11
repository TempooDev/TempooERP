using System.ComponentModel.DataAnnotations;
using TempooERP.BuildingBlocks.Application.Abstractions;

namespace TempooERP.Modules.Catalog.Application.Products.Commands.DeleteProduct;

public sealed record DeleteProductCommand(
   [Required ]
   Guid ProductId
) : ICommandEntity;