using Database;
using Database.Users;
using Microsoft.AspNetCore.Mvc;
using System.Security;

namespace Backend.Controllers
{
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ClinicContext _context;
        public LoginController(ClinicContext context)
        {
            _context = context;
        }
        [HttpGet("/Login/{email}/{password}")]
        public async Task<ActionResult<User>> CheckLoginData(string email, string password)
        {
            var user = _context.UserAccount.FirstOrDefault(p => p.Email == email && p.Password == password);
            if (user != null)
            {
                var userType = _context.Users.FirstOrDefault(p => p.UserAccountId == user.UserAccountId);
                return Ok(userType);
            }
            else
                return NotFound();
        }
    }
}
