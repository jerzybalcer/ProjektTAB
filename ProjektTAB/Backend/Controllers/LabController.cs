using Database;
using Database.Examinations;
using Database.Users;
using Database.Examinations.Simplified;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [ApiController]
    public class LabController : ControllerBase
    {
        private readonly ClinicContext _context;
        public LabController(ClinicContext context)
        {
            _context =context;  
        }
        [Authorize]
        [HttpGet("/GetAllOrderedLabExaminations")]
        public async Task<ActionResult<List<LabExamination>>> GetAllOrderedLabExaminations()
        {
            var examinations = await _context.LabExaminations
            .Where(p => p.Status == LabExaminationStatus.Ordered)
            .Include(p => p.Appointment)
            .ThenInclude(p=>p.Patient)
            .Include(p => p.ExaminationTemplate)
            .Include(p => p.LabAssistant)
            .ThenInclude(p=>p.UserAccount)
            .ToListAsync();
            if (!examinations.Any())
                return NotFound();
            else
                return Ok(examinations);
        }
        [Authorize]
        [HttpGet("/GetAllExecutedLabExaminations")]
        public async Task<ActionResult<List<LabExamination>>> GetAllExecutedExaminations()
        {
            var examinations = await _context.LabExaminations
            .Where(p => p.Status == LabExaminationStatus.SuccessfullyExecuted || p.Status == LabExaminationStatus.CancelledByAssistant)
            .Include(p => p.Appointment)
            .ThenInclude(p => p.Patient)
            .Include(p => p.ExaminationTemplate)
            .Include(p=>p.LabAssistant)
            .ThenInclude(p => p.UserAccount)
            .ToListAsync();
            if (!examinations.Any())
                return NotFound();
            else
                return Ok(examinations);
        }
        [Authorize]
        [HttpPost("/ChangeLabExaminationStatus")]
        public async Task<ActionResult> ChangeLabExaminationStatus(LabExaminationSimplified examination)
        {
            LabExamination _examination = _context.LabExaminations.Where(p => p.LabExaminationId == examination.LabExaminationId).FirstOrDefault();
            if (_examination == null)
                return NotFound();
            else
            {
                LabAssistant assistant = await _context.LabAssistants.Where(p => p.UserId == examination.LabAssistant.UserId).FirstOrDefaultAsync();
                _examination.LabAssistant = assistant;
                _examination.ExecutionDate = examination.ExecutionDate;
                _examination.Status = examination.Status;
                _examination.Result = examination.Result;
                await _context.SaveChangesAsync();
                return Ok();
            }
        }
        [Authorize]
        [HttpPost("/ChangeLabExaminationStatusByManager")]
        public async Task<ActionResult> ChangeLabExaminationStatusByManager(LabExaminationSimplified examination)
        {
            LabExamination _examination = await _context.LabExaminations.Where(p => p.LabExaminationId == examination.LabExaminationId).FirstOrDefaultAsync();
            if (_examination == null)
                return NotFound();
            else
            {
                LabManager manager = await  _context.LabManagers.Where(p => p.UserId == examination.LabManager.UserId).FirstOrDefaultAsync();
                _examination.LabManager = manager;
                _examination.ClosingDate = DateTime.Now;
                _examination.Status = examination.Status;
                _examination.LabManagerComment = examination.LabManagerComment;
                await _context.SaveChangesAsync();
                return Ok();
            }
        }
    }
}
