using JuniorDev.EntityFrameworkCore.Context;
using Microsoft.EntityFrameworkCore;
using Workouts.API.Models;

namespace Workouts.API.DatabaseOperations
{
    public class WorkoutContext : JDContext
    {
        // use another projects for workout WorkoutContext contex = new WorkoutContext("cnnStr");
        //public WorkoutContext(string connectringString)
        //{
            
        //}
        public WorkoutContext(DbContextOptions<WorkoutContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WorkoutContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

    }
}
