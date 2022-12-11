using System.ComponentModel.DataAnnotations;

namespace Workouts.API.Attibutes
{
    //used in Workouts.API/Models/Product
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class CustomRegexAttribute : Attribute
    {
        public string RegexPattern { get; set; }
        public string Message { get; set; }
    }
    //[RegularExpression] 
}
