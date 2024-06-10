using do_playlist_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace do_playlist_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistsController : ControllerBase
    {
        private readonly MusicDbContext _context;

        public PlaylistsController(MusicDbContext context)
        {
            _context = context;
        }

        // GET: api/Playlists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Playlist>>> GetPlaylists()
        {
            return await _context.Playlists.ToListAsync();
        }

        // GET: api/Playlists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Playlist>> GetPlaylist(int id)
        {
            var playlist = await _context.Playlists.FindAsync(id);

            if (playlist == null)
            {
                return NotFound();
            }

            return playlist;
        }

        // PUT: api/Playlists/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlaylist(int id, Playlist playlist)
        {
            if (id != playlist.Playlistid)
            {
                return BadRequest();
            }

            _context.Entry(playlist).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlaylistExists(id))
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

        // POST: api/Playlists
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Playlist>> PostPlaylist(Playlist playlist)
        {
            _context.Playlists.Add(playlist);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlaylist", new { id = playlist.Playlistid }, playlist);
        }

        // DELETE: api/Playlists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlaylist(int id)
        {
            var playlist = await _context.Playlists.Include(p => p.Canciones)
                                              .FirstOrDefaultAsync(p => p.Playlistid == id);
            if (playlist == null)
            {
                return NotFound();
            }
            
            if (playlist.Canciones.Any())
            {
                playlist.Canciones.Clear();
                await _context.SaveChangesAsync();
            }

            _context.Playlists.Remove(playlist);
            await _context.SaveChangesAsync();

            return NoContent();
        }




        // GET: api/Playlists/5/Canciones
        [HttpGet("{id}/Canciones")]
        public async Task<ActionResult<IEnumerable<Cancion>>> GetPlaylistsWithSongById(int id)
        {

            var playlist = await _context.Playlists
            .Include(p => p.Canciones)
            .FirstOrDefaultAsync(p => p.Playlistid == id);

            if (playlist == null)
            {
                return NotFound();
            }

            var result = new
            {
                playlist.Playlistid,
                playlist.Nombre,
                playlist.Descripcion,
                Canciones = playlist.Canciones.Select(c => new
                {
                    c.Cancionid,
                    c.Titulo,
                    c.Artista,
                    c.Album,
                    c.Genero,
                    c.Duracion
                }).ToList()
            };

            return Ok(result.Canciones);
        }


        // POST: api/Playlists/5/Canciones
        [HttpPost("{playlistId}/Canciones/{cancionId}")]
        public async Task<IActionResult> AddCancionToPlaylist(int playlistId, int cancionId)
        {
            var playlist = await _context.Playlists.Include(p => p.Canciones)
                                                   .FirstOrDefaultAsync(p => p.Playlistid == playlistId);

            if (playlist == null)
            {
                return NotFound("Playlist no encontrada.");
            }

            var existingCancion = await _context.Canciones.FirstOrDefaultAsync(c => c.Cancionid == cancionId);
            if (existingCancion != null)
            {
                playlist.Canciones.Add(existingCancion);
            }
            if(existingCancion != null)
            {
                return NotFound("Canción no encontrada.");
            }

            await _context.SaveChangesAsync();

            return Ok(200);
        }

        // DELETE: api/Playlists/5/Canciones/3
        [HttpDelete("{playlistId}/Canciones/{cancionId}")]
        public async Task<IActionResult> RemoveCancionFromPlaylist(int playlistId, int cancionId)
        {
            var playlist = await _context.Playlists.Include(p => p.Canciones)
                                                   .FirstOrDefaultAsync(p => p.Playlistid == playlistId);

            if (playlist == null)
            {
                return NotFound("Playlist no encontrada.");
            }


            var cancion = playlist.Canciones.FirstOrDefault(c => c.Cancionid == cancionId);
            if (cancion == null)
            if (cancion == null)
            {
                return NotFound("Canción no encontrada en la playlist.");
            }

            playlist.Canciones.Remove(cancion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlaylistExists(int id)
        {
            return _context.Playlists.Any(e => e.Playlistid == id);
        }


    }
}
