using Database;
using Database.Patients;
using Database.Users.Simplified;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Backend.Controllers
{
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
        [HttpGet("GetPatientById/{id}", Name = "GetPatientById")]
        public async Task<ActionResult<Patient>> GetPatientById(int id)
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
        [HttpPost("AddPatient")]
        public async Task<ActionResult<Patient>> AddPatient(Patient patient)
        {
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();

            return CreatedAtRoute(nameof(GetPatientById), new { id = patient.PatientId }, patient);
        }

    }
}
