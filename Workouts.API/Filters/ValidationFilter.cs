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
                //var errors = context.ModelState
                //    .Where(p => p.Value.Errors.Any())
                //    .ToDictionary(e => e.Key, e => e.Value.Errors.Select(s => s.ErrorMessage))
                //    .ToArray();

                List<string> errors = context.ModelState.Where(m => m.Value.Errors.Any())
                                                        .Select(x => x.Value.ToString())
                                                        .ToList();

                Response<bool> response = new Response<bool>();
                response.AddError(ResultType.ValidationException, errors);
                context.Result = new BadRequestObjectResult(response);
                return;
            }
            await next();
        }
    }
}
