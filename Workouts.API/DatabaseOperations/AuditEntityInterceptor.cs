using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Workouts.API.Models;

namespace Workouts.API.DatabaseOperations
{
    public class AuditEntityInterceptor : SaveChangesInterceptor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuditEntityInterceptor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
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
        private void Handle(EntityEntry<BaseEntity> entry)
        {
            var user = _httpContextAccessor.HttpContext.User;
            string userName = user?.Identity?.Name;

            var endpoint = _httpContextAccessor.HttpContext.GetEndpoint();

            bool allowAnonymous = endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null;

            if (string.IsNullOrEmpty(userName) && !allowAnonymous)
            {
                throw new Exception("User Info Not Found");
            }

            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedDate = DateTime.Now;
                entry.Entity.CreatedBy = userName;
                entry.Entity.ModifiedDate = null;
                entry.Entity.ModifiedBy = null;
                entry.Entity.IsDeleted = false;
            }

            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.ModifiedDate = DateTime.Now;
                entry.Entity.ModifiedBy = userName;
                entry.Entity.IsDeleted = false;
            }

            else if (entry.State == EntityState.Deleted /*&& entry.Entity is not IDeletableEntity*/)
            {
                entry.State = EntityState.Modified;
                entry.Entity.ModifiedDate = DateTime.Now;
                entry.Entity.ModifiedBy = userName;
                entry.Entity.IsDeleted = true;
            }
        }
    }
}
