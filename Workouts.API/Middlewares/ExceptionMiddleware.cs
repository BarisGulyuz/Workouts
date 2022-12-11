using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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

                httpContext.Response.ContentType = "application/json";

                Response<bool> contextResponse = new Response<bool>();

                if (ex is BusinessException) CreateResponseForException(httpContext, ex, HttpStatusCode.BadRequest, contextResponse, ResultType.BussinessException);
                else CreateResponseForException(httpContext, ex, HttpStatusCode.InternalServerError, contextResponse, ResultType.InternalServerError);

                await httpContext.Response.WriteAsync(contextResponse.ToString());
            }
        }

        private void CreateResponseForException(HttpContext httpContext, Exception ex, HttpStatusCode httpStatusCode, Response<bool> contextResponse, ResultType resultType)
        {
            httpContext.Response.StatusCode = (int)httpStatusCode;
            if (ex.Data.Count > 0)
            {
                List<string> errors = ex.Data.Values.Cast<string>().ToList();
                contextResponse.AddError(ResultType.BussinessException, errors);
            }
            else if (!string.IsNullOrEmpty(ex.Message))
            {
                contextResponse.Results.Add(new Result { ResultType = resultType, Message = ex.Message });
            }
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
