using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Workouts.API.DatabaseOperations;
using Workouts.API.Models;
using Workouts.API.Results.Response;

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

        [HttpPost]
        public async Task<IActionResult> Add(CreateCategoryDto categoryDto)
        {
            //check validation

            Category category = Category.CreateFromCategoryCreateDto(categoryDto);
            category.CreatedAt = DateTime.Now;
            category.ModifiedAt = DateTime.Now;
            category.IsActive = true;

            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();

            //this.Response
            return Created("", new Response("Added"));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCategoryDto updateCategoryDto)
        {
            //check validation

            Category category = dbContext.Categories.SingleOrDefault(c => c.Id == updateCategoryDto.Id);

            if (category == null)
            {
                return BadRequest(new Response("No data", ResultType.Error));
            }

            category.Name = updateCategoryDto.Name;
            category.ModifiedAt = DateTime.Now;

            await dbContext.SaveChangesAsync();

            return NoContent();
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int categoryId)
        {
            Category category = dbContext.Categories.FirstOrDefault(p => p.Id == categoryId);

            if (category is not null)
            {
                dbContext.Categories.Remove(category);
                await dbContext.SaveChangesAsync();
                return NoContent();
            }

            return BadRequest(new Response("Not Found", ResultType.Error));
        }

        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> SelectAll()
            => Ok(await dbContext.Categories.ToListAsync());

    }
}
