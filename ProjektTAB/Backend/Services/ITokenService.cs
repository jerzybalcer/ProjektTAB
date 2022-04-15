using Database.Users;
using System.Security.Claims;

namespace Backend.Services
{
    public interface ITokenService
    {
        public string GenerateToken(UserAccount userAccount);
        public string GenerateRefreshToken();
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
