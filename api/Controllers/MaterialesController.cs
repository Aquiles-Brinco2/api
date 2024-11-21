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
    public class MaterialesController : ControllerBase
    {
        private readonly PlataformaCursosContext _context;

        public MaterialesController(PlataformaCursosContext context)
        {
            _context = context;
        }

        // GET: api/Materiales
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Materiale>>> GetMateriales()
        {
            return await _context.Materiales.ToListAsync();
        }

        // GET: api/Materiales/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Materiale>> GetMateriale(int id)
        {
            var materiale = await _context.Materiales.FindAsync(id);

            if (materiale == null)
            {
                return NotFound();
            }

            return materiale;
        }

        // PUT: api/Materiales/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMateriale(int id, Materiale materiale)
        {
            if (id != materiale.Id)
            {
                return BadRequest();
            }

            _context.Entry(materiale).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaterialeExists(id))
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

        // POST: api/Materiales
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Materiale>> PostMateriale(Materiale materiale)
        {
            _context.Materiales.Add(materiale);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMateriale", new { id = materiale.Id }, materiale);
        }

        // DELETE: api/Materiales/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMateriale(int id)
        {
            var materiale = await _context.Materiales.FindAsync(id);
            if (materiale == null)
            {
                return NotFound();
            }

            _context.Materiales.Remove(materiale);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MaterialeExists(int id)
        {
            return _context.Materiales.Any(e => e.Id == id);
        }
    }
}
