namespace Workouts.API.Models
{
    public class User : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RefreshToken { get; set; }


        public static User CreateFromCreateUserDto(CreateUserDto createUserDto)
             => new User
             {
                 Name = createUserDto.Name,
                 Surname = createUserDto.Surname,
                 Email = createUserDto.Email,
             };
    }

    public record LoginDto(string Email,
                           string Password);

    public record CreateUserDto(string Name,
                                string Surname,
                                string Email,
                                string Password);

}
