namespace Workouts.API.JWT
{
    public record Token(string accessToken, string type, string expiresIn, string refreshToken);
}
