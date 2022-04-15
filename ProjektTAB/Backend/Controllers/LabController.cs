using Database;
using Database.Examinations;
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
        public async Task<ActionResult<LabExamination>> GetAllOrderedLabExaminations()
        {
            var examinations = await _context.LabExaminations
            .Where(p => p.Status == LabExaminationStatus.Ordered)
            .Include(p => p.Appointment)
            .ThenInclude(p=>p.Patient)
            .Include(p => p.ExaminationTemplate)
            .ToListAsync();
            if (!examinations.Any())
                return NotFound();
            else
                return Ok(examinations);
        }
        [Authorize]
        [HttpGet("/GetAllSuccessfullyExecutedLabExaminations")]
        public async Task<ActionResult<LabExamination>> GetAllSuccessfullyExecutedabExaminations()
        {
            var examinations = await _context.LabExaminations
            .Where(p => p.Status == LabExaminationStatus.SuccessfullyExecuted)
            .Include(p => p.Appointment)
            .ThenInclude(p => p.Patient)
            .Include(p => p.ExaminationTemplate)
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
                var assistant = _context.LabAssistants.Where(p => p.UserId == examination.LabAssistant.UserId);
                _examination.ExecutionDate = examination.ExecutionDate;
                _examination.ClosingDate = examination.ClosingDate;
                _examination.Status = examination.Status;
                _examination.Result = examination.Result;
                await _context.SaveChangesAsync();
                return Ok();
            }
        }
    }
}
