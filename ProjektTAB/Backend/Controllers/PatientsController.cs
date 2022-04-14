using Database;
using Database.Patients;
using Database.Users.Simplified;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly ClinicContext _context;
        public PatientsController(ClinicContext context)
        {
            _context = context;
        }

        // GET api/<PatientsController>/5
        [Authorize]
        [HttpGet("{id}", Name = "GetPatient")]
        public async Task<ActionResult<Patient>> GetPatient(int id)
        {
            var patient = await _context.Patients.Where(p => p.PatientId == id).FirstOrDefaultAsync();

            if (patient is null)
            {
                return NotFound();
            }
            else
            { 
                return Ok(patient);
            }
        }

        [Authorize(Roles = nameof(Role.Receptionist))]
        [HttpPost("Add")]
        public async Task<ActionResult<Patient>> AddPatient(Patient patient)
        {
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();

            return CreatedAtRoute(nameof(GetPatient), new { id = patient.PatientId }, patient);
        }

    }
}
