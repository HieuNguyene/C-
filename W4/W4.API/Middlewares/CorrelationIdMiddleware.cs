using Serilog.Context;
namespace W4.API.Middlewares
{
    public class CorrelationIdMiddleware
    {
        private readonly RequestDelegate _next;
        private const string CorrelationIdHeaderName = "X-Correlation-Id";

        public CorrelationIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            // Kiểm tra xem header có gửi correlationId hay không và có bị null không nếu không có thì tự động tạo một Guid làm correlationId
            var correclationId = context.Request.Headers.TryGetValue(CorrelationIdHeaderName, out var headerValue) && !string.IsNullOrWhiteSpace(headerValue)
            ? headerValue.ToString() : Guid.NewGuid().ToString();

            // Giúp các Controller hoặc ExceptionMiddleware phía sau có thể sử dụng mã một cách dễ dàng
            context.Items["CorrelationId"] = correclationId;

            // Trả ngược lại Header "X-correlationId" cho Client biết mã theo dõi request này.
            // Dùng context.Response.OnStarting để đảm bảo Header được gắn trước khi body được gửi về 
            context.Response.OnStarting(() =>
            {
                if (!context.Response.Headers.ContainsKey(CorrelationIdHeaderName))
                {
                    context.Response.Headers.Append(CorrelationIdHeaderName, correclationId);
                }
                return Task.CompletedTask;
            });

            // Dùng using: khi request kết thúc, thuộc tính này sẽ tự động được thu hồi (Dispose) không ảnh hưởng các Request khác chạy song song
            using (LogContext.PushProperty("CorrelationId", correclationId))
            {
                await _next(context);
            }
        }

    }
}