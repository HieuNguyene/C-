using System.Diagnostics;

namespace ProductManagment.Logging
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            _logger.LogInformation("Nhận Request: {Method} {URL}", context.Request.Method, context.Request.Path);
            var stopWatch = Stopwatch.StartNew();
            await _next(context);
            stopWatch.Stop();
            _logger.LogInformation("Trả Response: {StatusCode} mất {Elapsed} ms", context.Response.StatusCode, stopWatch.ElapsedMilliseconds);
        }
    }
}
