using System.ComponentModel.DataAnnotations;
using System.Net;
using Workouts.API.Results.Exceptions;
using Workouts.API.Results.Response;

namespace Workouts.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                var exceptionDetail = HandleException(ex);

                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)exceptionDetail.HttpStatusCode;

                Response contextResponse = new Response();
                contextResponse.AddResult(ResultType.Error, exceptionDetail.ExceptionMessage);

                await httpContext.Response.WriteAsJsonAsync(contextResponse);
            }
        }

        private (string ExceptionMessage, HttpStatusCode HttpStatusCode) HandleException(Exception exception)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            string exceptionMessage = "Something wrong, internal server error occured";

            if (exception is BusinessException || exception is ValidationException)
            {
                statusCode = HttpStatusCode.BadRequest;
                exceptionMessage = String.Join(",", exception.Data.Values);
            }

            return new(exceptionMessage, statusCode);
        }
    }

    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
