using Database;
using Database.Appointments;
using Database.Appointments.Simplified;
using Database.Users.Simplified;
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

        [Authorize(Roles = nameof(Role.Doctor))]
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

        [Authorize(Roles = nameof(Role.Doctor))]
        [HttpGet("/GetSelectedAppointment/{appointmentId}")]
        public async Task<ActionResult<AppointmentSimplified>> GetAppointment(int appointmentId)
        {
            var appointment = _context.Appointments
                .Include(d => d.Doctor)
                    .ThenInclude(ua => ua.UserAccount)
                .Include(r => r.Receptionist)
                    .ThenInclude(ua => ua.UserAccount)
                .Include(p => p.Patient)
                    .ThenInclude(a => a.Address)
                .Include(le => le.LabExaminations)
                .Include(pe => pe.PhysicalExaminations)
                .Where(p => p.AppointmentId == appointmentId)
                .FirstOrDefault();

            var doctor = _context.Doctors
                .Include(ua => ua.UserAccount)
                .Where(id => id.UserId == appointment.Doctor.UserId)
                .Select(l => new UserSimplified(l.UserId, l.Name, l.Surname, l.UserAccount.Email, l.UserAccountId, Role.Doctor, l.UserAccount.IsActive, l.LicenseNumber))
                .FirstOrDefault();


            var receptionist = _context.Receptionists
                .Include(ua => ua.UserAccount)
                .Where(d => d.UserId == appointment.Receptionist.UserId)
                .Select(l => new UserSimplified(l.UserId, l.Name, l.Surname, l.UserAccount.Email, l.UserAccountId, Role.Receptionist, l.UserAccount.IsActive, null))
                .FirstOrDefault();



            
            var toSend = new AppointmentSimplified
             {
                 AppointmentId = appointment.AppointmentId,
                 Description = appointment.Description,
                 Diagnosis = appointment.Diagnosis,
                 Status = appointment.Status,
                 RegistrationDate = appointment.RegistrationDate,
                 ClosingDate = appointment.ClosingDate,
                 Patient = appointment.Patient,
                 PhysicalExaminations = appointment.PhysicalExaminations,
                 LabExaminations = appointment.LabExaminations,
                 Doctor = doctor,
                 Receptionist = receptionist
             };

            return Ok(toSend);
        }

        [Authorize]
        [HttpPost("/SaveAppointmentData")]
        public async Task<ActionResult> SaveAppointmentData(AppointmentSimplified appointment)
        {
            Appointment _appointment = _context.Appointments
                .Include(d => d.Doctor)
                .Include(p => p.Patient)
                .ThenInclude(a => a.Address)
                .Include(r => r.Receptionist)
                .Where(a => a.AppointmentId == appointment.AppointmentId)
                .FirstOrDefault();
            if (_appointment == null)
                return NotFound();
            else
            {
                _appointment.Status = appointment.Status;
                _appointment.Description = appointment.Description;
                _appointment.Diagnosis = appointment.Diagnosis;
                await _context.SaveChangesAsync();
                return Ok();
            }
        }

    }
}
