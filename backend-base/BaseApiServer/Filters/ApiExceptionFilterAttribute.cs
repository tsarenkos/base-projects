using Base.Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Base.ApiServer.Filters
{
    public class ApiExceptionFilterAttribute : IExceptionFilter
    {
        private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

        public ApiExceptionFilterAttribute()
        {
            this._exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>()
            {
                { typeof(NotFoundException), this.HandleNotFoundException },
                { typeof(ApplicationValidationException), this.HandleValidationException },
            };
        }

        private void HandleNotFoundException(ExceptionContext context)
        {
            var exception = (NotFoundException)context.Exception;

            var details = new ProblemDetails
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                Title = "Not found error(s) occurred.",
                Status = StatusCodes.Status404NotFound,
                Detail = exception.Message,
                Instance = context.HttpContext.Request.Path
            };

            context.Result = new NotFoundObjectResult(details);
            context.ExceptionHandled = true;
        }

        private void HandleValidationException(ExceptionContext context)
        {
            var exception = (ApplicationValidationException)context.Exception;

            var details = new ValidationProblemDetails(exception.Errors)
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Title = "Validation failed.",
                Status = StatusCodes.Status400BadRequest,
                Detail = exception.Message,
                Instance = context.HttpContext.Request.Path
            };

            context.Result = new BadRequestObjectResult(details);
            context.ExceptionHandled = true;
        }

        public void OnException(ExceptionContext context)
        {
            var type = context.Exception.GetType();
            if (this._exceptionHandlers.TryGetValue(type, out var action))
            {
                action.Invoke(context);
                return;
            }

            this.HandleUndefinedException(context);
        }

        private void HandleUndefinedException(ExceptionContext context)
        {
            var exception = context.Exception;

            var details = new ProblemDetails()
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                Title = "Undefined error(s) occurred.",
                Status = StatusCodes.Status500InternalServerError,
                Detail = exception.Message,
                Instance = context.HttpContext.Request.Path
            };

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            context.ExceptionHandled = true;
        }
    }
}
