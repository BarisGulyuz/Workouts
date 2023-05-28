using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Workouts.API.DatabaseOperations;

namespace Workouts.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly WorkoutContext dbContext;

        public CategoriesController(WorkoutContext context)
        {
            dbContext = context;
        }

        [HttpGet]
        public async Task<IActionResult> SelectAll() => Ok(await dbContext.Categories.ToListAsync());

    }
}
