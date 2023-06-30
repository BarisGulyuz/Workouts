using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Workouts.API.Results.Response;

namespace Workouts.API.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                List<string> errors = context.ModelState.Where(m => m.Value.Errors.Any())
                                                        .Select(x => x.Value.ToString())
                                                        .ToList();

                Response response = new Response();
                response.AddResult(ResultType.ValidationError, errors);
                context.Result = new BadRequestObjectResult(response);
                return;
            }
            await next();
        }
    }
}
