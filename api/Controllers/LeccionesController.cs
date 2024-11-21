using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Models;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeccionesController : ControllerBase
    {
        private readonly PlataformaCursosContext _context;

        public LeccionesController(PlataformaCursosContext context)
        {
            _context = context;
        }

        // GET: api/Lecciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Leccione>>> GetLecciones()
        {
            return await _context.Lecciones.ToListAsync();
        }

        // GET: api/Lecciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Leccione>> GetLeccione(int id)
        {
            var leccione = await _context.Lecciones.FindAsync(id);

            if (leccione == null)
            {
                return NotFound();
            }

            return leccione;
        }

        // PUT: api/Lecciones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLeccione(int id, Leccione leccione)
        {
            if (id != leccione.Id)
            {
                return BadRequest();
            }

            _context.Entry(leccione).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeccioneExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Lecciones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Leccione>> PostLeccione(Leccione leccione)
        {
            _context.Lecciones.Add(leccione);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLeccione", new { id = leccione.Id }, leccione);
        }

        // DELETE: api/Lecciones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeccione(int id)
        {
            var leccione = await _context.Lecciones.FindAsync(id);
            if (leccione == null)
            {
                return NotFound();
            }

            _context.Lecciones.Remove(leccione);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LeccioneExists(int id)
        {
            return _context.Lecciones.Any(e => e.Id == id);
        }
    }
}
