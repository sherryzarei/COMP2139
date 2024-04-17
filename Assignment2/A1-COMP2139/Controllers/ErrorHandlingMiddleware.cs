using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace A1_COMP2139.Controllers
{
    public class ErrorHandlingMiddleware : Controller
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception has occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Set the status code for the HTTP response
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            // You can also serve a specific error view if you want:
            // context.Response.Redirect("/Error");

            // Or write a custom response:
            return context.Response.WriteAsync("An error occurred processing your request.");
        }
    }
}