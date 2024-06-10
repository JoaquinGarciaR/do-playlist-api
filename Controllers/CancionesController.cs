using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using do_playlist_api.Models;

namespace do_playlist_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CancionesController : ControllerBase
    {
        private readonly MusicDbContext _context;

        public CancionesController(MusicDbContext context)
        {
            _context = context;
        }

        // GET: api/Canciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cancion>>> GetCanciones()
        {
            return await _context.Canciones.ToListAsync();
        }

        // GET: api/Canciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cancion>> GetCanciones(int id)
        {
            var canciones = await _context.Canciones.FindAsync(id);

            if (canciones == null)
            {
                return NotFound();
            }

            return canciones;
        }

        // PUT: api/Canciones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCanciones(int id, CancionInput cancion)
        {
            var canciones = await _context.Canciones.FindAsync(id);
            // Actualizar solo los campos necesarios
            canciones.Titulo = cancion.Titulo;
            canciones.Artista = cancion.Artista;
            canciones.Album = cancion.Album;
            canciones.Genero = cancion.Genero;
            canciones.Duracion = new TimeOnly(0, cancion.Minutes, cancion.Seconds);

            if (id != canciones.Cancionid)
            {
                return BadRequest();
            }



            _context.Entry(canciones).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CancionesExists(id))
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

        // POST: api/Canciones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cancion>> PostCanciones(CancionInput cancion)
        {

            Cancion canciones = new Cancion
            {
                Titulo = cancion.Titulo,
                Artista = cancion.Artista,
                Album = cancion.Album,
                Genero = cancion.Genero,
                Duracion = new TimeOnly(0, cancion.Minutes, cancion.Seconds)
            };
            Console.WriteLine(cancion);
            Console.WriteLine(canciones);
            _context.Canciones.Add(canciones);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCanciones", new { id = canciones.Cancionid }, canciones);
        }

        // DELETE: api/Canciones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCanciones(int id)
        {
            var canciones = await _context.Canciones.FindAsync(id);
            if (canciones == null)
            {
                return NotFound();
            }

            _context.Canciones.Remove(canciones);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CancionesExists(int id)
        {
            return _context.Canciones.Any(e => e.Cancionid == id);
        }
    }
}
