//using JuniorDev.EntityFrameworkCore.DatabaseManager;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Workouts.API.DatabaseOperations;
//using Workouts.API.Models;

//namespace Workouts.API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class WorkoutsController : ControllerBase
//    {
//        private readonly WorkoutContext dbContext;
//        IServiceProvider serviceProvider;
//        public WorkoutsController(WorkoutContext context, IServiceProvider serviceProvider)
//        {
//            dbContext = context;
//            this.serviceProvider = serviceProvider;
//        }
//        [HttpPost]
//        public async Task<IActionResult> Work()
//        {
//            DatabaseManager databaseManager = new DatabaseManager(dbContext, serviceProvider);

//            _ = await dbContext.Set<Category>().AddAsync(new Category { Id = 1, Name = "Elma" });

//            databaseManager.StartInNew((context) =>
//            {
//                context.Set<Category>().AddAsync(new Category { Name = "Elma" });
//            });

//            await dbContext.SaveChangesAsync();

//            return Ok();
//        }
//    }
//}
