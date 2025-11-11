using System.Net;
using System.Text.Encodings.Web;
using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace TempooERP.BuildingBlocks.API.Middleware;

public sealed class ExceptionHandlingMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        WriteIndented = true
    };

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "application/json";

            var errors = ex.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => ToCamelCase(g.Key),
                    g => g.Select(e => e.ErrorMessage).ToArray()
                );

            var payload = new
            {
                success = false,
                message = "Validation failed",
                errors
            };

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(
                    payload,
                    _jsonOptions));
        }
        catch (Exception)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var payload = new
            {
                success = false,
                message = "Unexpected error"
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(payload, _jsonOptions));
        }
    }

    private static string ToCamelCase(string name) =>
        string.IsNullOrEmpty(name)
        || char.IsLower(name[0])
            ? name
            : char.ToLowerInvariant(name[0]) + name[1..];
}
