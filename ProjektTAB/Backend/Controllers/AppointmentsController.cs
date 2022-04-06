using Microsoft.AspNetCore.Mvc;
using Database;
using Database.Users;
using Database.Patients;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly ClinicContext _context;
        public AppointmentsController(ClinicContext context)
        {
            _context = context;
        }
        [HttpGet("/GetListOfDoctors")]
        public async Task<ActionResult<Doctor>> GetAllDoctors()
        {
            var doctors = _context.Doctors.ToList();
            if (doctors == null)
                return NotFound();
            else
            {
                doctors.ForEach(p =>
                {
                    var account = _context.UserAccounts.FirstOrDefault(g => g.UserAccountId == p.UserAccountId);
                    p.UserAccount = account;
                });
                return Ok(doctors);
            }
        }
        [HttpGet("/GetAllAvailablesDates/{idDoctor}/{day}")]
        public async Task<ActionResult<List<string>>> GetAllAvailableDates(int idDoctor,DateTime day)
        {
            List<string> availableDates = new List<string>() { "8:00", "8:30", "9:00", "9:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "15:00", "15:30" };
            List<Appointment> allAppointments = _context.Appointments.Where(p=>p.Doctor.UserId == idDoctor && p.RegistrationDate.Day == day.Day && p.RegistrationDate.Month == day.Month && p.RegistrationDate.Year == day.Year).ToList();
            if (allAppointments.Count==0)
            {
                return Ok(availableDates);
            }
            return Ok(availableDates);
        }
        [HttpGet("/SearchPatients/{data}",Name ="GetPatients")]
        public async Task<ActionResult<List<Patient>>> GetPatients(string data)
        {
            var patients = _context.Patients.Where(p => 
            EF.Functions.Like(p.Pesel, "%" + data+"%")||
            EF.Functions.Like(p.Surname, "%" + data + "%")||
            EF.Functions.Like(p.Name, "%" + data + "%")
            );
            if (patients.Count() != 0)
                return Ok(patients);
            else
                return NotFound();
        }
        [HttpPost("/AddAppointment")]
        public async Task<ActionResult> AddAppointment(Appointment appointment)
        {
        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();
        return CreatedAtRoute(nameof(GetPatients), new {id = appointment.AppointmentId},appointment);
        }
    }
}
