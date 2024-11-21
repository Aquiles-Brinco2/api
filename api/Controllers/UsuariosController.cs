using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Models;
using Microsoft.AspNetCore.Identity.Data;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly PlataformaCursosContext _context;

        public UsuariosController(PlataformaCursosContext context)
        {
            _context = context;
        }




        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            if (loginRequest == null || string.IsNullOrEmpty(loginRequest.Email) || string.IsNullOrEmpty(loginRequest.Password))
            {
                return BadRequest("Por favor ingrese ambos el email y la contraseña.");
            }

            // Cambiar la comparación a ToLower()
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email.ToLower() == loginRequest.Email.ToLower());

            if (usuario == null)
            {
                return Unauthorized("Usuario no encontrado.");
            }

            // Verificar si la contraseña coincide
            if (usuario.Password != loginRequest.Password)  // Asegúrate de que la comparación de contraseñas es segura (esto es solo un ejemplo)
            {
                return Unauthorized("Contraseña incorrecta.");
            }

            // Si todo es correcto, devolver información del usuario o un token (aquí lo simplificamos solo con los datos básicos)
            return Ok(new
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Email = usuario.Email,
                Tipo = usuario.Tipo
            });
        }



        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            if (registerRequest == null || string.IsNullOrEmpty(registerRequest.Email) || string.IsNullOrEmpty(registerRequest.Password) || string.IsNullOrEmpty(registerRequest.Nombre))
            {
                return BadRequest("Por favor, complete todos los campos requeridos.");
            }

            // Verificar si el email ya está registrado
            if (await _context.Usuarios.AnyAsync(u => u.Email.ToLower() == registerRequest.Email.ToLower()))
            {
                return BadRequest("El email ya está registrado.");
            }

            // Crear el nuevo usuario
            var nuevoUsuario = new Usuario
            {
                Nombre = registerRequest.Nombre,
                Email = registerRequest.Email.ToLower(),
                Password = registerRequest.Password, // Nota: Usa un hash seguro para almacenar contraseñas
                Tipo = registerRequest.Tipo ?? "Estudiante", // Valor predeterminado
                FechaRegistro = DateTime.UtcNow
            };

            // Si se proporciona una imagen, guardarla
            if (!string.IsNullOrEmpty(registerRequest.ImagenBase64))
            {
                nuevoUsuario.Imagen = Convert.FromBase64String(registerRequest.ImagenBase64);
            }

            _context.Usuarios.Add(nuevoUsuario);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "Usuario registrado exitosamente.",
                UsuarioId = nuevoUsuario.Id
            });
        }






        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }





        // PUT: api/Usuarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
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

        // POST: api/Usuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuario", new { id = usuario.Id }, usuario);
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }
    }
}
