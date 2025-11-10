namespace TempooERP.BuildingBlocks.Application.Abstractions;

public interface IQueryPagedEntity
{
    int Page { get; }
    int PageSize { get; }
    string? SortBy { get; }
    string? SortDirection { get; }
}