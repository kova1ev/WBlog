using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WBlog.Core.Exceptions;

namespace WBlog.Admin.Filters;

public class ApiExceptionFilter : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        HandlerExceptions(context);
        base.OnException(context);
    }

    private static void HandlerExceptions(ExceptionContext context)
    {
        Exception exception = context.Exception;
        if (exception is ObjectExistingException)
        {
            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status409Conflict,
                Title = "Conflict",
                Detail = exception.Message
            };
            context.Result = new ConflictObjectResult(problemDetails);
            context.ExceptionHandled = true;
        }
        else if (exception is ObjectNotFoundException)
        {
            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Not Found",
                Detail = exception.Message
            };
            context.Result = new NotFoundObjectResult(problemDetails);
            context.ExceptionHandled = true;
        }
    }
}