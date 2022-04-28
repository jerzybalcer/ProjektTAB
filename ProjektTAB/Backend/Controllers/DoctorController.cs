using Database;
using Database.Appointments;
using Database.Appointments.Simplified;
using Database.Examinations;
using Database.Examinations.Simplified;
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
        [Authorize]
        [HttpGet("/GetExaminations/{patientId}")]
        public async Task<ActionResult<Tuple<List<PhysicalExaminationSimplified>, List<LabExaminationSimplified>>>> GetExaminations(int patientId)
        {

            var PhysicalExaminations = await _context.PhysicalExaminations
                .Include(app => app.Appointment).ThenInclude(a => a.Patient)
                .Include(ex => ex.ExaminationTemplate)
                .Where(a => a.Appointment.Patient.PatientId == patientId)
                .OrderByDescending(ex => ex.Appointment.RegistrationDate)
                .Select(phy => new PhysicalExaminationSimplified(phy.PhysicalExaminationId, phy.Appointment.AppointmentId, phy.ExaminationTemplate, phy.Result, phy.Appointment.RegistrationDate))
                .ToListAsync();

            var LabExaminations = await _context.LabExaminations
                .Include(app => app.Appointment).ThenInclude(a => a.Patient)
                .Include(temp => temp.ExaminationTemplate)
                .Include(ass => ass.LabAssistant)
                    .ThenInclude(acc => acc.UserAccount)
                .Include(men => men.LabManager)
                    .ThenInclude(acc => acc.UserAccount)
                .Where(a => a.Appointment.Patient.PatientId == patientId)
                .OrderByDescending(ex => ex.OrderDate)
                .Select(l => new LabExaminationSimplified {
                    ClosingDate = l.ClosingDate,
                    ExaminationTemplate = l.ExaminationTemplate,
                    ExecutionDate = l.ExecutionDate,
                    LabAssistant = l.LabAssistant == null ? null : new UserSimplified(l.LabAssistant.UserId, l.LabAssistant.Name, l.LabAssistant.Surname, l.LabAssistant.UserAccount.Email, l.LabAssistant.UserAccountId, Role.LabAssistant, l.LabAssistant.UserAccount.IsActive, null),
                    LabExaminationId = l.LabExaminationId,
                    LabManager = l.LabManager == null ? null : new UserSimplified(l.LabManager.UserId, l.LabManager.Name, l.LabManager.Surname, l.LabManager.UserAccount.Email, l.LabManager.UserAccountId, Role.LabManager, l.LabManager.UserAccount.IsActive, null),
                    LabManagerComment = l.LabManagerComment,
                    OrderDate = l.OrderDate,
                    Result = l.Result,
                    Status = l.Status
                })
                .ToListAsync();

            return Ok(new Tuple<List<PhysicalExaminationSimplified>, List<LabExaminationSimplified>>(PhysicalExaminations, LabExaminations));
        }

        [Authorize(Roles = nameof(Role.Doctor))]
        [HttpGet("/GetExaminationsTypes")]
        public async Task<ActionResult<List<ExaminationTemplate>>> GetExaminationsTypes()
        {
            
            var examinations = await _context.ExaminationTemplates
            .ToListAsync();
            return Ok(examinations);
            
        }

        [Authorize]
        [HttpPost("/AddLabExamination")]
        public async Task<ActionResult> SaveAppointmentData(Tuple<string, int> tuple)
        {

            var template = _context.ExaminationTemplates
                .FirstOrDefault(temp => temp.ExaminationCode == tuple.Item1);

            var appointment = _context.Appointments
                .FirstOrDefault(appointment => appointment.AppointmentId == tuple.Item2);

            DateTime localDate = DateTime.Now;
            LabExamination toAdd = new LabExamination(localDate, appointment, template, null);

            _context.LabExaminations.Add(toAdd);
            await _context.SaveChangesAsync();

            return Ok();
        }

        public struct dataToSend
        {
            public int AppointmentId;
            public string templateType;
            public string description;
        }

        [Authorize]
        [HttpPost("/AddPhysicalExamination")]
        public async Task<ActionResult> AddPhysicalExamination(dataToSend data)
        {

            var template = _context.ExaminationTemplates
                .FirstOrDefault(temp => temp.ExaminationCode == data.templateType);

            var appointment = _context.Appointments
                .FirstOrDefault(appointment => appointment.AppointmentId == data.AppointmentId);

            DateTime localDate = DateTime.Now;
            PhysicalExamination toAdd = new PhysicalExamination(appointment, template, data.description);

            _context.PhysicalExaminations.Add(toAdd);
            await _context.SaveChangesAsync();

            return Ok();
        }


    }
}
