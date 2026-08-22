using System.Text.Json;

namespace ProductManagment.Middleware
{
    public class ExceptionMiddleware
    {

        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                //rq người dùng
                await _next(context);
            }
            catch (Exception e) {
                {
                    _logger.LogError(e, "Có lỗi xảy ra");
                    int statusCode = e switch
                    {
                        KeyNotFoundException => StatusCodes.Status404NotFound,
                        ArgumentException => StatusCodes.Status400BadRequest,
                        InvalidOperationException => StatusCodes.Status409Conflict,
                         _ => StatusCodes.Status500InternalServerError
                    };
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = statusCode;
                    var error = new
                    {
                        Message = "Hệ thống xảy ra sự cố",
                        Detail = e.Message,
                    };
                    await context.Response.WriteAsync(JsonSerializer.Serialize(error));
                }
            }
        }
    }
}
