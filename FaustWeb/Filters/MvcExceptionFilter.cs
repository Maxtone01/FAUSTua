using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace FaustWeb.Filters;

public class MvcExceptionFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {

    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception != null)
        {
            var referer = context.HttpContext.Request.Headers["Referer"].ToString();

            context.Result = new RedirectResult(referer);
            context.ExceptionHandled = true;
        }
    }
}
