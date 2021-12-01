using Microsoft.AspNetCore.Builder;

namespace MoviesApp
{
    public static class MiddlewareExtantion
    {
        public static IApplicationBuilder UseRequestLog(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestLoggingMiddleware>();
        }
    }
}