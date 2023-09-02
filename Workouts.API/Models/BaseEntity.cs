using System.ComponentModel.DataAnnotations.Schema;

namespace Workouts.API.Models
{
    public class BaseEntity
    {
        [Column(Order = 0)]
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }

        public bool IsDeleted { get; set; }
    }

    public interface IDeletableEntity { }
}
