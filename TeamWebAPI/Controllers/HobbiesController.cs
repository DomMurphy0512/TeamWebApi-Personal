using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HobbiesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public HobbiesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id?}")]
        public async Task<ActionResult<IEnumerable<Hobby>>> GetHobbies(int? id)
        {
            if (id == null || id == 0)
                return await _context.Hobbies.Take(5).ToListAsync();

            var hobby = await _context.Hobbies.FindAsync(id);
            if (hobby == null)
                return NotFound();

            return Ok(hobby);
        }

        [HttpPost]
        public async Task<ActionResult<Hobby>> PostHobby(Hobby hobby)
        {
            _context.Hobbies.Add(hobby);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetHobbies), new { id = hobby.Id }, hobby);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutHobby(int id, Hobby hobby)
        {
            if (id != hobby.Id)
                return BadRequest();

            _context.Entry(hobby).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HobbyExists(id))
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHobby(int id)
        {
            var hobby = await _context.Hobbies.FindAsync(id);
            if (hobby == null)
                return NotFound();

            _context.Hobbies.Remove(hobby);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HobbyExists(int id)
        {
            return _context.Hobbies.Any(e => e.Id == id);
        }
    }
}
