using Backend.Services;
using Database;
using Database.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Microsoft.EntityFrameworkCore;
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
        public async Task<ActionResult<UserSimplified>> CheckLoginData(string email, string password)
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
}
