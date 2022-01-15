using VideoGames.WebApi.Middleware.Exceptions;

namespace VideoGames.WebApi.Middleware;

internal static class MiddlewareExtensions
{
    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomExceptionHandler>();
    }
}
