namespace Workouts.API.Models
{
    public class BaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public bool IsActive { get; set; }
    }

    public interface IDeletableEntity { }
}
