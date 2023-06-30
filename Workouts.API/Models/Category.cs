namespace Workouts.API.Models
{
    public class Category : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Product> Products { get; set; }

        public static Category CreateFromCategoryCreateDto(CreateCategoryDto categoryCreateDto)
        {
            return new Category { Name = categoryCreateDto.Name };
        }
        public static Category CreateFromCategoryUpdateDto(UpdateCategoryDto categoryUpdateDto)
        {
            return new Category { Name = categoryUpdateDto.Name };
        }
        //public static UpdateCategoryDto ToCategoryUpdateDto(Category category)
        //{
        //    return new UpdateCategoryDto(Id : category.Id, Name: category.Name);
        //}
    }

    public record CreateCategoryDto(string Name);
    public record UpdateCategoryDto(int Id, string Name);
}
