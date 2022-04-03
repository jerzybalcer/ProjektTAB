using Database;
using Database.Users;
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
        public LoginController(ClinicContext context)
        {
            _context = context;
        }
        [HttpGet("/Login/{email}/{password}")]
        public async Task<ActionResult<User>> CheckLoginData(string email, string password)
        {
            var user = _context.UserAccounts.FirstOrDefault(p => p.Email == email && p.Password == password);
            if (user != null)
            {
                var userType = _context.Users.FirstOrDefault(p => p.UserAccountId == user.UserAccountId);
                var jsonSettings = JsonConfiguration.GetJsonSettings();
                var json = JsonConvert.SerializeObject(userType, jsonSettings);
                return Ok(json);
            }
            else
                return NotFound();
        }
    }
}
