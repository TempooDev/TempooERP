using TempooERP.BuildingBlocks.Application.Abstractions;

namespace TempooERP.Modules.Sales.Application.Orders.Queries.GetOrderById;

public sealed record GetOrderByIdQuery(Guid Id) : IQueryEntity;
