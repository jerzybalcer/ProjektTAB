using Backend.Services;
using Database;
using Database.Users;
using Database.Users.Simplified;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ClinicContext _context;
        private readonly ITokenService _tokenService;

        public LoginController(ClinicContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [Authorize]
        [HttpGet("/GetUser/{email}/{password}")]
        public async Task<ActionResult<UserSimplified>> GetUser(string email, string password)
        {
            string decryptedPassword = password.Replace("%2F", "/");
            decryptedPassword = decryptedPassword.Decrypt();
            var simpleUser = await _context.UserAccounts
                .Include(u => u.User)
                .Where(u => u.Email == email && u.Password == decryptedPassword)
                .Select(u => new UserSimplified()
                {
                    UserId = u.User.UserId,
                    Name = u.User.Name,
                    Surname = u.User.Surname,
                    Email = u.Email,
                    AccountId = u.UserAccountId,
                    IsActive = u.IsActive,
                }).FirstOrDefaultAsync();

            User? user = await _context.Users
                .Where(u => u.UserId == simpleUser.UserId).FirstOrDefaultAsync();

            if(user is not null)
            {
                Enum.TryParse(user.GetType().Name, out Role role);
                simpleUser.Role = role;

                if(role == Role.Doctor)
                {
                    simpleUser.LicenseNumber = (user as Doctor).LicenseNumber;
                }

                return Ok(simpleUser);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("/Login")]
        [AllowAnonymous]
        public ActionResult<string> Login(UserLogin userLogin)
        {
            string decryptedPassword = userLogin.Password.Replace("%2F", "/");
            decryptedPassword = decryptedPassword.Decrypt();
            userLogin.Email = userLogin.Email.Replace("%40", "@");
            var matchingAccount = _context.UserAccounts.Include(u => u.User)
                .Where(acc => acc.Email == userLogin.Email && acc.Password == decryptedPassword)
                .FirstOrDefault();

            if(matchingAccount == null || !matchingAccount.IsActive)
            {
                return NotFound();
            }
            else
            {
                var token = _tokenService.GenerateToken(matchingAccount);
                return Ok(token);
            }
        }
    }
}
