using Database;
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
            var workers = await _context.Users.ToListAsync();

            var admins = await _context.Admins.ToListAsync();

            workers = workers.Except(admins).ToList();

            return Ok(workers);
        }
    }
}
