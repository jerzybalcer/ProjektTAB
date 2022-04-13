using Database;
using Database.Patients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly ClinicContext _context;
        public PatientsController(ClinicContext context)
        {
            _context = context;
        }

        // GET: api/<PatientsController>
        [Authorize]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<PatientsController>/5
        [Authorize]
        [HttpGet("{id}",Name ="Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PatientsController>
        [Authorize]
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [Authorize]
        [HttpPost("Add")]
        public async Task<ActionResult<Patient>> PostTodoItem(Patient patient)
        {
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
            return CreatedAtRoute(nameof(Get), new { id = patient.PatientId }, patient);
        }

        // PUT api/<PatientsController>/5
        [Authorize]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PatientsController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
