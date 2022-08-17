using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Dotnet_API.Exceptions;

public class HttpExceptionFilter : IActionFilter, IOrderedFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception is HttpException httpException)
        {
            context.Result = new ObjectResult(httpException.Value)
            {
                StatusCode = httpException.StatusCode
            };

            context.ExceptionHandled = true;
        }
        else
        {
            context.Result = new ObjectResult(null)
            {
                StatusCode = 500
            };

            context.ExceptionHandled = true;
        }
    }

    public int Order => int.MaxValue - 10;
}