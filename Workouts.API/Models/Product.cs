using Workouts.API.Attibutes;

namespace Workouts.API.Models
{
    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        [CustomRegex(RegexPattern ="2021|2022", Message ="Procution Year Can Be Only 2022 and 2023")]
        public int ProductionYear { get; set; }


        //must in another class or must be in pipeline etc.

        public bool Validate()
        {
            return true;
        }
    }
}
