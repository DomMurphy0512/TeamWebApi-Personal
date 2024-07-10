using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteFoodsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FavoriteFoodsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id?}")]
        public async Task<ActionResult<IEnumerable<FavoriteFood>>> GetFavoriteFoods(int? id)
        {
            if (id == null || id == 0)
                return await _context.FavoriteFoods.Take(5).ToListAsync();

            var favoriteFood = await _context.FavoriteFoods.FindAsync(id);
            if (favoriteFood == null)
                return NotFound();

            return Ok(favoriteFood);
        }

        [HttpPost]
        public async Task<ActionResult<FavoriteFood>> PostFavoriteFood(FavoriteFood favoriteFood)
        {
            _context.FavoriteFoods.Add(favoriteFood);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFavoriteFoods), new { id = favoriteFood.Id }, favoriteFood);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutFavoriteFood(int id, FavoriteFood favoriteFood)
        {
            if (id != favoriteFood.Id)
                return BadRequest();

            _context.Entry(favoriteFood).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FavoriteFoodExists(id))
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFavoriteFood(int id)
        {
            var favoriteFood = await _context.FavoriteFoods.FindAsync(id);
            if (favoriteFood == null)
                return NotFound();

            _context.FavoriteFoods.Remove(favoriteFood);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FavoriteFoodExists(int id)
        {
            return _context.FavoriteFoods.Any(e => e.Id == id);
        }
    }
}
