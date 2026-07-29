using System.Diagnostics;

namespace W4.middleware
{
    public class CustomeMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomeMiddleware(RequestDelegate next) { 
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            Console.WriteLine("----RESQUEST----");
            Console.WriteLine($"Method : {context.Request.Method}");
            Console.WriteLine($"Path : {context.Request.Path}");
            Console.WriteLine($"Time : {DateTime.Now}");

            await _next(context);

            Console.WriteLine("----RESPONSE----");
            Console.WriteLine($"Status: {context.Response.StatusCode}");
            Console.WriteLine($"Excution time: {stopwatch.ElapsedMilliseconds}ms");

        }
    }
}
