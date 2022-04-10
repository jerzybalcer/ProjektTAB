using Microsoft.AspNetCore.Mvc;
using Database;
using Database.Users;
using Database.Patients;
using Microsoft.EntityFrameworkCore;
using Database.Users.Simplified;
using Database.Appointments.Simplified;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly ClinicContext _context;
        public AppointmentsController(ClinicContext context)
        {
            _context = context;
        }
        [HttpGet("/GetListOfDoctors")]
        public async Task<ActionResult<List<Doctor>>> GetAllDoctors()
        {
            var doctors = await _context.Doctors
                .Include(d => d.UserAccount)
                .Select(d => new UserSimplified()
                {
                    UserId = d.UserId,
                    Name = d.Name,
                    Surname = d.Surname,
                    Email = d.UserAccount.Email,
                    AccountId = d.UserAccountId,
                    Role = Role.Doctor,
                    IsActive = d.UserAccount.IsActive,
                    LicenseNumber = d.LicenseNumber
                })
                .ToListAsync();
            return Ok(doctors);
        }


        [HttpGet("/GetAllAvailablesDates/{idDoctor}/{day}")]
        public async Task<ActionResult<List<string>>> GetAllAvailableDates(int idDoctor, DateTime day)
        {
            List<string> availableDates = new List<string>() { "8:00", "8:30", "9:00", "9:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "15:00", "15:30" };
            List<Appointment> allAppointments = _context.Appointments.Where(p => p.Doctor.UserId == idDoctor && p.RegistrationDate.Day == day.Day && p.RegistrationDate.Month == day.Month && p.RegistrationDate.Year == day.Year).ToList();
            if (allAppointments.Count==0)
            {
                return Ok(availableDates);
            }
            return Ok(availableDates);
        }
        [HttpGet("/SearchPatients/{data}", Name = "GetPatients")]
        public async Task<ActionResult<List<Patient>>> GetPatients(string data)
        {
            var patients = _context.Patients.Where(p =>
            EF.Functions.Like(p.Pesel, "%" + data+"%")||
            EF.Functions.Like(p.Surname, "%" + data + "%")||
            EF.Functions.Like(p.Name, "%" + data + "%")
            ).Include(p => p.Address).ToList();

            if (patients.Count() != 0)
                return Ok(patients);
            else
                return NotFound();
        }
        [HttpPost("Add")]
        public async Task<IActionResult> AddAppointment(AppointmentSimplified simpleAppointment)
        {

            var receptionist = _context.Receptionists
                .Where(r => r.UserId == simpleAppointment.Receptionist.UserId)
                .FirstOrDefault();

            var doctor = _context.Doctors
                .Where(d => d.UserId == simpleAppointment.Doctor.UserId)
                .FirstOrDefault();

            var patient = _context.Patients
                .Where(p => p.PatientId == simpleAppointment.Patient.PatientId)
                .FirstOrDefault();

            Appointment newAppointment = new Appointment(simpleAppointment.RegistrationDate, receptionist, doctor, patient);

            _context.Appointments.Add(newAppointment);
            await _context.SaveChangesAsync();

            return CreatedAtRoute(nameof(GetById), new { id = newAppointment.AppointmentId }, newAppointment);
        }

        [HttpGet("/GetById/{id}")]
        public async Task<ActionResult<Appointment>> GetById(int id)
        {
            var appointment = await _context.Appointments.Where(p => p.AppointmentId == id).FirstOrDefaultAsync();

            if (appointment == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(appointment);
            }
        }
    }
}
