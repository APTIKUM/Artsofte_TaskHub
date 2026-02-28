using System.Diagnostics;

namespace Api.Middlewares
{
    public class ResponseTimeMiddleware
    {
        private readonly RequestDelegate _next;

        public ResponseTimeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopWatch = Stopwatch.StartNew();

            context.Response.OnStarting(() =>
            {
                stopWatch.Stop();
                context.Response.Headers["X-Response-Time-Ms"]
                = stopWatch.ElapsedMilliseconds.ToString();
                return Task.CompletedTask;
            });

            await _next(context);
        }

    }
}
