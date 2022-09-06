using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WBlog.Application.Core.Exceptions;

namespace WBlog.Web.Api.Filters
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {

        public override void OnException(ExceptionContext context)
        {
            Handlerexceptions(context);
            base.OnException(context);
        }


        private void Handlerexceptions(ExceptionContext context)
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
            else if (exception is ObjectNotFoundExeption)
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
}