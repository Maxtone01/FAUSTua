using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace FaustWeb.Filters;

public class ApiControllerExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        context.Result = new BadRequestObjectResult(new
        {
            Error = context.Exception.Message,
            Trace = context.Exception.StackTrace
        });

        context.ExceptionHandled = true;
    }
}
