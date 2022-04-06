using Backend.Services;
using Database;
using Database.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Newtonsoft.Json;
using System.Security;

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

        [HttpGet("/Login/{email}/{password}")]
        public async Task<ActionResult<User>> CheckLoginData(string email, string password)
        {
            var user = _context.UserAccounts.FirstOrDefault(p => p.Email == email && p.Password == password);
            if (user != null)
            {
                var userType = _context.Users.FirstOrDefault(p => p.UserAccountId == user.UserAccountId);
                var jsonSettings = JsonConfiguration.GetJsonSettings();
                var json = JsonConvert.SerializeObject(userType,userType.GetType(), jsonSettings);
                return Ok(json);
            }
            else
                return NotFound();
        }

        [HttpPost("/Login")]
        [AllowAnonymous]
        public IActionResult Login(UserLogin userLogin)
        {
            UserAccount? matchingAccount = _context.UserAccounts
                .Where(acc => acc.Email == userLogin.Email && acc.Password == userLogin.Password)
                .FirstOrDefault();

            if(matchingAccount == null)
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
