using Microsoft.AspNetCore.Mvc;
using Workouts.API.DatabaseOperations;
using Workouts.API.JWT;
using Workouts.API.Models;

namespace Workouts.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly WorkoutContext _workoutContext;
        private readonly JWTTokenGenerator _jwtTokenGenerator;

        public AuthController(JWTTokenGenerator jwtTokenGenerator, WorkoutContext workoutContext)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _workoutContext = workoutContext;
        }

        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser(CreateUserDto createUserDto)
        {
            //validations

            User user = Models.User.CreateFromCreateUserDto(createUserDto);
            user.Password = createUserDto.Password; // hash password ...

            await _workoutContext.AddAsync(user);
            await _workoutContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn(LoginDto loginDto)
        {
            User user = _workoutContext.Users
                                        .FirstOrDefault(u => u.Email == loginDto.Email
                                                          && u.Password == loginDto.Password); // hash password before this and check....

            if (user == null)
                return Unauthorized();

            var token = _jwtTokenGenerator.GenerateToken(user);

            user.RefreshToken = token.refreshToken;
            await _workoutContext.SaveChangesAsync();

            return Ok();
        }
    }
}
