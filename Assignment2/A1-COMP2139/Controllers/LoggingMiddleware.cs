using Microsoft.AspNetCore.Mvc;

namespace A1_COMP2139.Controllers
{
    public class LoggingMiddleware : Controller
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
            // Log the incoming request
            _logger.LogInformation("Handling request: " + context.Request.Path);

            // Call the next middleware in the pipeline
            await _next(context);

            // Log the outgoing response
            _logger.LogInformation("Finished handling request.");
        }
    }
}
