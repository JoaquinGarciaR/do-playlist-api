using System;
using System.Collections.Generic;

namespace do_playlist_api.Models;

public partial class Playlist
{
    public int Playlistid { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public virtual ICollection<Cancion> Cancion { get; set; } = new List<Cancion>();
}
