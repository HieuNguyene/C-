using System.Text.Json;
using W3.Responses;

namespace W4.middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                if(ex is KeyNotFoundException)
                {
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                } 
                else
                { 
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                }

                    var response = new ApiResponse<object>
                    {
                        Success = false,
                        Message = ex.Message,
                        Data = null
                    };
                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                
            }
        }
        }
    }
