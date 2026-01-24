using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace portfolioBackend.Services
{
    // Service responsible for generating JWT tokens for authenticated users
    public class TokenService
    {
        private readonly IConfiguration _config;

        // Constructor to inject application configuration (used to read JWT settings)
        public TokenService(IConfiguration config)
        {
            _config = config;
        }

        // Generates a JWT token using user id and email
        public string GenerateToken(string userid, string email)
        {
            // Create claims that will be stored inside the token
            var claims = new[]
            {
                // Subject claim usually holds the unique user identifier
                new Claim(JwtRegisteredClaimNames.Sub, userid),

                // Email claim stores the user's email
                new Claim(JwtRegisteredClaimNames.Email, email)
            };

            // Read the secret key from configuration and create a symmetric security key
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"])
            );

            // Create signing credentials using HMAC SHA256 algorithm
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Create the JWT token with issuer, audience, claims, expiry, and signing credentials
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2), // Token will expire after 2 hours
                signingCredentials: creds
            );

            // Convert the token to a string and return it
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
