using TempooERP.BuildingBlocks.Application.Abstractions;

namespace TempooERP.Modules.Catalog.Application.Products.Queries.GetById;

public sealed record GetProductByIdQuery(
    Guid Id): IQueryEntity;
