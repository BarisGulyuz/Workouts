using System.ComponentModel.DataAnnotations;

namespace Workouts.API.Models
{
    public class Product : BaseEntity, IDeletableEntity
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        //[CustomRegex(RegexPattern ="2021|2022", Message ="Procution Year Can Be Only 2022 and 2023")]
        [RegularExpression("2022|20223")]
        public int ProductionYear { get; set; }

        public Category Category { get; set; }
    }
}
