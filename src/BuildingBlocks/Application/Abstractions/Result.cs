namespace TempooERP.BuildingBlocks.Application.Abstractions;

public sealed record Result
{
    public bool Success { get; init; }
    public string? Message { get; init; }
    public Dictionary<string, string[]>? Errors { get; init; }

    public static Result Ok(string? message = null) =>
        new()
        {
            Success = true,
            Message = message
        };

    public static Result Fail(string message, Dictionary<string, string[]>? errors = null) =>
        new()
        {
            Success = false,
            Message = message,
            Errors = errors
        };
}

#pragma warning disable CA1000 // Do not declare static members on generic types
public sealed record Result<T>
{
    public bool Success { get; init; }
    public string? Message { get; init; }
    public T? Data { get; init; }
    public Dictionary<string, string[]>? Errors { get; init; }

    public static Result<T> Ok(T data, string? message = null) =>
        new()
        {
            Success = true,
            Message = message,
            Data = data
        };

    public static Result<T> Fail(string message, Dictionary<string, string[]>? errors = null) =>
        new()
        {
            Success = false,
            Message = message,
            Errors = errors
        };
}
