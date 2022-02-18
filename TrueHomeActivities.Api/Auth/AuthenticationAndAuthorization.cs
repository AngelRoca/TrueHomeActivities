using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TrueHomeActivities.Api.Models;

namespace TrueHomeActivities.Api.Auth
{
    public class AuthenticationAndAuthorization
    {
        private readonly IConfiguration _configuration;

        public AuthenticationAndAuthorization(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string? GrantAccess(User request)
        {
            var user = Authenticate(request);

            if (user == null)
            {
                return null;
            }

            return Authorize(user);
        }

        private User? Authenticate(User request)
        {
            return UsersRepository.Users
                .FirstOrDefault(user => user.UserName == request.UserName && user.Password == request.Password);
        }

        private string Authorize(User request)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, request.UserName),
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
