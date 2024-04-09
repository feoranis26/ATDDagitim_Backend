using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ATDBackend.Security
{
    public static class TokenHandler
    {
        public static Token CreateToken(IConfiguration configuration, int userId)
        {
            Token token = new();

            SymmetricSecurityKey key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SECURITYKEY"))
            );

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            token.Expiration = DateTime
                .Now
                .AddMinutes(Convert.ToInt16(configuration["JWTToken:ExpirationMins"]));

            var claims = new[] { new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()), };
            JwtSecurityToken jwtToken =
                new(
                    issuer: configuration["JWTToken:Issuer"],
                    audience: configuration["JWTToken:Audience"],
                    claims: claims,
                    expires: token.Expiration,
                    notBefore: DateTime.Now,
                    signingCredentials: creds
                );

            JwtSecurityTokenHandler tokenHandler = new();
            token.AccessToken = tokenHandler.WriteToken(jwtToken);

            byte[] numbers = new byte[32];
            using RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(numbers);
            token.RefreshToken = Convert.ToBase64String(numbers);

            return token;
        }
    }
}
