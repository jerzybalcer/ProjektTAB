using Database.Users;

namespace Backend.Services
{
    public interface ITokenService
    {
        public string GenerateToken(UserAccount userAccount);
        public bool IsTokenValid();
    }
}
