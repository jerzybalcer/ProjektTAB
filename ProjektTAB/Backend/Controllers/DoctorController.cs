using Database;
using Database.Appointments;
using Database.Appointments.Simplified;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {

        private readonly ClinicContext _context;
        public DoctorController(ClinicContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("/GetDoctorsAppointments/{doctorId}")]
        public async Task<ActionResult<List<AppointmentSimplified>>> GetAppointments(int doctorId)
        {
            var appointments = await _context.Appointments
                .Include(p => p.Patient)
                .Where(a => a.Doctor.UserAccountId == doctorId && (a.Status == AppointmentStatus.Registered || a.Status == AppointmentStatus.Started))
                .OrderBy(d => d.RegistrationDate)
                .ToListAsync();
            return Ok(appointments);
        }

    }
}
