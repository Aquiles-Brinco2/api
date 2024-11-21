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
    public class CursoesController : ControllerBase
    {
        private readonly PlataformaCursosContext _context;

        public CursoesController(PlataformaCursosContext context)
        {
            _context = context;
        }



        [HttpGet("MisCursos")]
        public async Task<IActionResult> MisCursos([FromQuery] int userId)
        {
            if (userId <= 0)
            {
                return BadRequest("El ID de usuario no es válido.");
            }

            var misCursos = await (from ins in _context.Inscripciones
                                   join curso in _context.Cursos on ins.IdCurso equals curso.Id
                                   join calif in _context.Calificaciones on new { ins.IdCurso, ins.IdUsuario } equals new { calif.IdCurso, calif.IdUsuario } into calificacionesGroup
                                   from calif in calificacionesGroup.DefaultIfEmpty() // Manejar si no hay calificación
                                   where ins.IdUsuario == userId
                                   select new MisCursosViewModel
                                   {
                                       CursoId = curso.Id,
                                       TituloCurso = curso.Titulo,
                                       DescripcionCurso = curso.Descripcion,
                                       FechaInscripcion = ins.FechaInscripcion, // nullable DateTime?
                                       Calificacion = calif != null ? (int?)calif.Calificacion : null // nullable int?, manejar si no hay calificación
                                   }).ToListAsync();

            if (misCursos == null || !misCursos.Any())
            {
                return NotFound("No se encontraron cursos para este usuario.");
            }

            return Ok(misCursos);  // Devuelve la lista de cursos
        }
    





    // GET: api/Cursoes
    [HttpGet]
        public async Task<ActionResult<IEnumerable<Curso>>> GetCursos()
        {
            return await _context.Cursos.ToListAsync();
        }

        // GET: api/Cursoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Curso>> GetCurso(int id)
        {
            var curso = await _context.Cursos.FindAsync(id);

            if (curso == null)
            {
                return NotFound();
            }

            return curso;
        }

        // PUT: api/Cursoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCurso(int id, Curso curso)
        {
            if (id != curso.Id)
            {
                return BadRequest();
            }

            _context.Entry(curso).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CursoExists(id))
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

        // POST: api/Cursoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Curso>> PostCurso(Curso curso)
        {
            _context.Cursos.Add(curso);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCurso", new { id = curso.Id }, curso);
        }

        // DELETE: api/Cursoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurso(int id)
        {
            var curso = await _context.Cursos.FindAsync(id);
            if (curso == null)
            {
                return NotFound();
            }

            _context.Cursos.Remove(curso);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CursoExists(int id)
        {
            return _context.Cursos.Any(e => e.Id == id);
        }
    }
}
