using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Workouts.API.Models;

namespace Workouts.API.JWT
{
    public class JWTTokenGenerator
    {
        private readonly JWTSettings _jwtBearerSettings;

        public JWTTokenGenerator(IOptions<JWTSettings> jwtBearerSettingsOptions)
        {
            _jwtBearerSettings = jwtBearerSettingsOptions.Value;
        }
        public Token GenerateToken(User user)
        {
            DateTime now = DateTime.Now;
            DateTime expiry = DateTime.Now.Add(TimeSpan.FromHours(1));

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtBearerSettings.SigningKey));
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken securityToken = new JwtSecurityToken
            (
                issuer: _jwtBearerSettings.Issuer,
                audience: _jwtBearerSettings.Audience,
                claims: claims,
                notBefore: now,
                expires: expiry,
                signingCredentials: signingCredentials
            );

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            string token = tokenHandler.WriteToken(securityToken);

            return new Token
                         (token,
                         JwtBearerDefaults.AuthenticationScheme,
                         expiry.Subtract(DateTime.UtcNow).TotalSeconds.ToString("0"),
                         GenereateRefreshToken());
        }

        private string GenereateRefreshToken() => Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
    }
}
