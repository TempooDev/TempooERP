using TempooERP.BuildingBlocks.Application.Abstractions;

namespace TempooERP.BuildingBlocks.Application.Extensions;

public static class PagedResultExtensions
{
    public static Result<PagedResult<T>> ToResult<T>(
        this PagedResult<T> paged,
        string? message = null) => Result<PagedResult<T>>.Ok(paged, message);
}
