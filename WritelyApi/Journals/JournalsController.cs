using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WritelyApi.Helpers;

namespace WritelyApi.Journals
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class JournalsController : ControllerBase
    {
        private readonly IJournalService _service;

        public JournalsController(IJournalService service)
        {
            _service = service;
        }

        // Retrieve all of the simple journals 
        // for the user.
        // GET - api/journals
        [HttpGet]
        public async Task<IActionResult> GetAllJournals([FromServices] ICurrentUserService service)
        {
            if (ModelState.IsValid)
            {
                var user = await service.GetCurrentUser(HttpContext);
                var result = await _service.GetJournals(user.Id);
                return Ok(result);
            }

            return BadRequest(ModelState);
        }

        // Retrieve a single journal, as well as its entries.
        // GET - api/journals/{journalId}
        [HttpGet("{journalId}")]
        public async Task<IActionResult> GetJournal(int journalId)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.FindById(journalId);
                return Ok(result);
            }

            return BadRequest(ModelState);
        }

        // Create a new journal
        // POST - api/journals
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] string title,
            [FromServices] ICurrentUserService service)
        {
            if (ModelState.IsValid)
            {
                var user = await service.GetCurrentUser(HttpContext);
                var result = await _service.Create(title, user.Id);

                return Ok(result);
            }

            return BadRequest(ModelState);
        }

        // Update a journal.
        // PUT - api/journals
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] JournalDto journal)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.Update(journal);
                return Ok(result);
            }

            return BadRequest(ModelState);
        }

        // Delete a given journal.
        // DELETE - api/journals/{journalId}
        [HttpDelete("{journalId}")]
        public async Task<IActionResult> Delete(int journalId)
        {
            if (ModelState.IsValid)
            {
                await _service.Delete(journalId);
                return Ok();
            }

            return BadRequest(ModelState);
        }
    }
}