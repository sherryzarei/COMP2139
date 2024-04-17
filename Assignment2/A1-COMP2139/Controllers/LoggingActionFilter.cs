using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace A1_COMP2139.Controllers
{
    public class LoggingActionFilter : ActionFilterAttribute
    {
        private readonly ILogger<LoggingActionFilter> _logger;

        public LoggingActionFilter(ILogger<LoggingActionFilter> logger)
        {
            _logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Log details of the incoming request
            _logger.LogInformation("Executing action: {ActionName}", context.ActionDescriptor.DisplayName);
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            // Log details after the action has executed
            _logger.LogInformation("Executed action: {ActionName}", context.ActionDescriptor.DisplayName);
            base.OnActionExecuted(context);
        }
    }
}
