using Backend.Services;
using Database;
using Database.Users;
using Database.Users.Simplified;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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
            var simpleUser = await _context.UserAccounts
                .Include(u => u.User)
                .Where(u => u.Email == email && u.Password == password)
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

            if (user is not null)
            {
                Enum.TryParse(user.GetType().Name, out Role role);
                simpleUser.Role = role;

                if (role == Role.Doctor)
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
        public async Task<ActionResult<TokensPair>> Login(UserLogin userLogin)
        {

            userLogin.Email = userLogin.Email.Replace("%40", "@");

            var matchingAccount = await _context.UserAccounts.Include(u => u.User)
                .Where(acc => acc.Email == userLogin.Email && acc.Password == userLogin.Password)
                .FirstOrDefaultAsync();

            if (matchingAccount == null || !matchingAccount.IsActive)
            {
                return NotFound();
            }
            else
            {
                var token = _tokenService.GenerateToken(matchingAccount);

                matchingAccount.RefreshToken = _tokenService.GenerateRefreshToken();
                matchingAccount.RefreshTokenExpiries = DateTime.Now.AddHours(9);

                await _context.SaveChangesAsync();

                return Ok(new TokensPair
                {
                    Token = token,
                    RefreshToken = matchingAccount.RefreshToken
                });
            }
        }

        [AllowAnonymous]
        [HttpPost("RefreshToken")]
        public async Task<ActionResult<TokensPair>> RefreshToken(TokensPair tokensPair)
        {
            if (tokensPair is null)
            {
                return BadRequest("Invalid tokens pair object");
            }

            var principal = _tokenService.GetPrincipalFromExpiredToken(tokensPair.Token);

            var userEmail = principal.FindFirst(x => x.Type == ClaimTypes.Email)?.Value;

            var userAccount = await _context.UserAccounts.Include(u => u.User)
                .Where(u => u.Email == userEmail).FirstOrDefaultAsync();

            if (userAccount == null)
            {
                return NotFound("User not found");
            }
            else if (userAccount.RefreshToken != tokensPair.RefreshToken || userAccount.RefreshTokenExpiries <= DateTime.Now)
            {
                return Unauthorized("Refresh token not valid");
            }

            var newToken = _tokenService.GenerateToken(userAccount);

            userAccount.RefreshToken = _tokenService.GenerateRefreshToken();
            userAccount.RefreshTokenExpiries = DateTime.Now.AddHours(9);

            await _context.SaveChangesAsync();

            return Ok(new TokensPair {
                Token = newToken,
                RefreshToken = userAccount.RefreshToken
            });
        }

        //[Authorize]
        //[HttpPost("ChangePassword")]
        //public async Task<IActionResult> ChangePassword(string newPassword)
        //{
        //    var principal = _tokenService.GetPrincipalFromExpiredToken(Request.Headers.Authorization);

        //    var userEmail = principal.FindFirst(x => x.Type == ClaimTypes.Email)?.Value;

        //    var userAccount = await _context.UserAccounts.Include(u => u.User)
        //        .Where(u => u.Email == userEmail).FirstOrDefaultAsync();

        //    if (userAccount == null)
        //    {
        //        return NotFound("Could not find account");
        //    }

        //    userAccount.Password = newPassword;

        //    await _context.SaveChangesAsync();

        //    return Ok();
        //}
    }
}
