using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.IO;
using System.Text;

namespace MoviesApp
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, ILogger<RequestLoggingMiddleware> logger)
        {
            if (httpContext.Request.Path.ToString().Contains("Artist"))
            {
                logger.LogTrace($"Request: {httpContext.Request.Path}  Method: {httpContext.Request.Method}");
            }
            await _next(httpContext);
            
        }
    }
}