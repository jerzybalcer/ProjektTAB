using Database;
using Database.Users;
using Database.Users.Simplified;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ClinicContext _context;

        public UsersController(ClinicContext context)
        {
            _context = context;
        }

        [HttpGet("GetWorkers")]
        public async Task<IActionResult> GetWorkers()
        {
            var workers = await _context.Users.Include(u => u.UserAccount).ToListAsync();

            var admins = await _context.Admins.ToListAsync();

            workers = workers.Except(admins).ToList();

            var simplifiedWorkers = workers.Select(w => new UserSimplified
            {
                UserId = w.UserId,
                Name = w.Name,
                Surname = w.Surname,
                Email = w.UserAccount.Email,
                AccountId = w.UserAccountId,
                IsActive = w.UserAccount.IsActive
            }).ToList();

            for(var i = 0; i< workers.Count; i++)
            {
                Enum.TryParse(workers[i].GetType().Name, out Role role);
                simplifiedWorkers[i].Role = role;

                if (role == Role.Doctor)
                {
                    Doctor doc = (Doctor)workers[i];
                    simplifiedWorkers[i].LicenseNumber = doc.LicenseNumber;
                }
            }

            return Ok(simplifiedWorkers);
        }

        [HttpPost("AddWorker")]
        public async Task<IActionResult> AddWorker(UserSimplified userSimplified)
        {
            var account = new UserAccount(userSimplified.Email, userSimplified.Name + "" + userSimplified.Surname);
            User? user = null;

            if(userSimplified.Role == Role.Doctor)
            {
                user = new Doctor(userSimplified.Name, userSimplified.Surname, account, userSimplified.LicenseNumber);
            }
            else if(userSimplified.Role == Role.Receptionist)
            {
                user = new Receptionist(userSimplified.Name, userSimplified.Surname, account);
            }
            else if (userSimplified.Role == Role.LabAssistant)
            {
                user = new LabAssistant(userSimplified.Name, userSimplified.Surname, account);
            }
            else if (userSimplified.Role == Role.LabManager)
            {
                user = new LabManager(userSimplified.Name, userSimplified.Surname, account);
            }
            else
            {
                return BadRequest("Invalid role");
            }

            if(user == null)
            {
                return BadRequest("Could not create user");
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("UpdateWorker")]
        public async Task<IActionResult> UpdateWorker(UserSimplified userSimplified)
        {
            var oldUser = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userSimplified.UserId);

            if(oldUser == null)
            {
                return NotFound("User not found");
            }

            var oldAccount = await _context.UserAccounts.FirstOrDefaultAsync(ua => ua.UserAccountId == userSimplified.AccountId);

            if (oldAccount == null)
            {
                return NotFound("Account not found");
            }

            Enum.TryParse(oldUser.GetType().Name, out Role oldRole);

            if(oldRole == userSimplified.Role)
            {
                oldUser.Name = userSimplified.Name;
                oldUser.Surname = userSimplified.Surname;

                if(oldRole == Role.Doctor)
                {
                    (oldUser as Doctor).LicenseNumber = userSimplified.LicenseNumber;
                }
            }
            else
            {
                if (userSimplified.Role == Role.Doctor)
                {
                    oldUser = new Doctor(userSimplified.Name, userSimplified.Surname, oldAccount, userSimplified.LicenseNumber);
                }
                else if (userSimplified.Role == Role.Receptionist)
                {
                    oldUser = new Receptionist(userSimplified.Name, userSimplified.Surname, oldAccount);
                }
                else if (userSimplified.Role == Role.LabAssistant)
                {
                    oldUser = new LabAssistant(userSimplified.Name, userSimplified.Surname, oldAccount);
                }
                else if (userSimplified.Role == Role.LabManager)
                {
                    oldUser = new LabManager(userSimplified.Name, userSimplified.Surname, oldAccount);
                }
                else
                {
                    return BadRequest("Invalid role");
                }

                oldAccount.User = oldUser;
                oldUser.UserAccount = oldAccount;
            }

            oldUser.UserAccount.Email = userSimplified.Email;
            oldUser.UserAccount.IsActive = userSimplified.IsActive;

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
