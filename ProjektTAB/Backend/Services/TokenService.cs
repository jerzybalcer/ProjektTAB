using Database;
using Database.Users;
using Database.Users.Simplified;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Backend.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly ClinicContext _clinicContext;

        public TokenService(IConfiguration configuration, ClinicContext clinicContext)
        {
            _configuration = configuration;
            _clinicContext = clinicContext;
        }

        public string GenerateToken(UserAccount userAccount)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            Enum.TryParse(userAccount.User.GetType().Name, out Role role);

            var claims = new[] {
                new Claim(ClaimTypes.Email, userAccount.Email),
                new Claim(ClaimTypes.Role, role.ToString())
            };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool IsTokenValid()
        {
            throw new NotImplementedException();
        }
    }
}
