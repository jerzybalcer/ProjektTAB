using Microsoft.AspNetCore.Mvc;
using Database;
using Database.Users;
using Database.Patients;
using Microsoft.EntityFrameworkCore;
using Database.Users.Simplified;
using Database.Appointments.Simplified;
using Microsoft.AspNetCore.Authorization;

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

        [Authorize(Roles = nameof(Role.Receptionist))]
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

        [Authorize(Roles = nameof(Role.Receptionist))]
        [HttpGet("/GetAllAvailablesDates/{idDoctor}/{day}/{month}/{year}")]
        public async Task<ActionResult<List<string>>> GetAllAvailableDates(int idDoctor, int day,int month, int year)
        {
            List<string> availableDatesString = new List<string>();
            DateTime simple = new DateTime(year,month,day,8,0,0);
            if (simple.DayOfWeek == DayOfWeek.Sunday || simple.DayOfWeek == DayOfWeek.Saturday)
                return NotFound();
            if (DateTime.Compare(simple, DateTime.Now) < 0)
                return NotFound();
            List<DateTime> availableDates = new List<DateTime>();
            for (int i = 0; i < 16; i++)
                availableDates.Add(simple.AddMinutes(i*30));
            List<Appointment> allAppointments = _context.Appointments
           .Where(p => p.Doctor.UserId == idDoctor 
            && p.RegistrationDate.Day == day 
            && p.RegistrationDate.Month == month 
            && p.RegistrationDate.Year == year)
            .ToList();
           foreach(var appointment in allAppointments)
           {
                var itemToRemove = availableDates.Single
                (p =>
                p.Hour == appointment.RegistrationDate.Hour &&
                p.Minute == appointment.RegistrationDate.Minute
                );
                DateTime dateToRemove = new DateTime(year, month, day, itemToRemove.Hour,itemToRemove.Minute, 0);
                availableDates.Remove(dateToRemove);
           }
            availableDates.ForEach(p =>
            {
                if(p.Minute ==0)
                    availableDatesString.Add(p.Hour + ":" + p.Minute+"0");
                else
                    availableDatesString.Add(p.Hour + ":" + p.Minute);
            });
            if (availableDatesString.Count == 0)
                return NotFound();
            return Ok(availableDatesString);
        }

        [Authorize(Roles = nameof(Role.Receptionist))]
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

        [Authorize(Roles = nameof(Role.Receptionist))]
        [HttpPost("AddAppointment")]
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

            return CreatedAtRoute(nameof(GetByAppointmentId), new { id = newAppointment.AppointmentId }, newAppointment);
        }

        [Authorize(Roles = nameof(Role.Receptionist))]
        [HttpGet("/GetAppointmentById/{id}",Name ="GetByAppointmentId")]
        public async Task<ActionResult<Appointment>> GetByAppointmentId(int id)
        {
            var appointment = await _context.Appointments.Where(p => p.AppointmentId == id).FirstOrDefaultAsync();

            if (appointment == null)
                return NotFound();
            else
                return Ok(appointment);
        }
    }
}
