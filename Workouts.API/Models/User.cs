namespace Workouts.API.Models
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RefreshToken { get; set; }


        public static User CreateFromCreateUserDto(CreateUserDto createUserDto)
             => new User
             {
                 UserName = createUserDto.UserName,
                 Email = createUserDto.Email,
             };
    }

    public record LoginDto(string Email,
                           string Password);

    public record CreateUserDto(string UserName,
                                string Email,
                                string Password);

}
