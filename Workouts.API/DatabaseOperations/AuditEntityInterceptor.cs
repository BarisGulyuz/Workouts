using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Workouts.API.Models;

namespace Workouts.API.DatabaseOperations
{
    public class AuditEntityInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            WorkoutContext workoutContext = (WorkoutContext)eventData.Context;

            if (workoutContext != null)
            {
                var entries = workoutContext.ChangeTracker.Entries<BaseEntity>();

                foreach (var entry in entries)
                {
                    Handle(entry);
                }
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private static void Handle(EntityEntry<BaseEntity> entry)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = DateTime.Now;
                entry.Entity.ModifiedAt = null;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.ModifiedAt = DateTime.Now;
            }
            else if (entry.State == EntityState.Deleted /*&& entry.Entity is not IDeletableEntity*/)
            {
                entry.State = EntityState.Modified;
                entry.Entity.ModifiedAt = DateTime.Now;
                entry.Entity.IsActive = false;
            }
        }
    }
}
