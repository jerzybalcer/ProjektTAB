using Database;
using Database.Examinations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Backend.Controllers
{
    [ApiController]
    public class ExaminationController : ControllerBase
    {
        private readonly ClinicContext _context;
        public ExaminationController(ClinicContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("/GetExaminationTemplateById/{id}", Name ="GetExaminationTemplateById")]
        public async Task<ActionResult<ExaminationTemplate>> GetExaminationTemplateById(string id)
        {
            var examinationTemplate = await _context.ExaminationTemplates.Where(p => p.ExaminationCode == id).FirstOrDefaultAsync();
            if (examinationTemplate == null)
                return NotFound();
            else
                return Ok(examinationTemplate);
        }

        [Authorize]
        [HttpPost("/AddExaminationTemplate")]
        public async Task<ActionResult> AddExaminationTemplate(ExaminationTemplate template)
        {
            _context.ExaminationTemplates.Add(template);
            await _context.SaveChangesAsync();
            return CreatedAtRoute(nameof(GetExaminationTemplateById), new { id= template.ExaminationCode }, template);
        }

    }
}
