using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WritelyApi.Data;

namespace WritelyApi.Entries
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EntriesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IEntryService _service;

        public EntriesController(AppDbContext context, IEntryService service)
        {
            _context = context;
            _service = service;
        }

        // Add Entry
        // POST - api/entries
        [HttpPost]
        public async Task<IActionResult> AddEntry(EntryCreationDto dto)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.Add(dto);

                if (result != null) return Ok(result);

                return BadRequest(new { Message = "Unable to add entry" });
            }

            return BadRequest(ModelState);
        }

        // Update Entry
        // PUT - api/entries/{id}
        [HttpPut]
        public async Task<IActionResult> UpdateEntry(EntryDto dto)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.Update(dto);
                if (result != null) return Ok(result);

                return BadRequest(new { Message = "Unable to update entry" });
            }

            return BadRequest(ModelState);
        }

        // Delete Entry
        // DELETE - api/entries
        [HttpDelete]
        public async Task<IActionResult> DeleteEntry(EntryDeletionDto dto)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.Delete(dto);

                if (result > 0) return Ok();

                return BadRequest(new { Message = "Unable to delete entry" });
            }

            return BadRequest(ModelState);
        }
    }
}