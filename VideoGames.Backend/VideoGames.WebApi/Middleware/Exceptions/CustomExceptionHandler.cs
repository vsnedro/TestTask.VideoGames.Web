using System.Net;
using System.Net.Mime;
using System.Text.Json;

using Microsoft.AspNetCore.Connections;

using VideoGames.Application.Exceptions;

namespace VideoGames.WebApi.Middleware.Exceptions;

internal class CustomExceptionHandler
{
    private readonly RequestDelegate _next;

    public CustomExceptionHandler(RequestDelegate next) =>
        _next = next ?? throw new ArgumentNullException(nameof(next));

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var responseCode = HttpStatusCode.InternalServerError;
        var responseText = string.Empty;

        switch (exception)
        {
            case EntityNotFoundException:
                responseCode = HttpStatusCode.BadRequest;
                break;

            case OperationCanceledException:
            case ConnectionResetException:
                responseCode = HttpStatusCode.NoContent;
                break;

            default:
                responseText = JsonSerializer.Serialize(new { error = exception.Message });
                break;
        }

        context.Response.ContentType = MediaTypeNames.Application.Json;
        context.Response.StatusCode = (int)responseCode;

        await context.Response.WriteAsync(responseText);
    }
}
