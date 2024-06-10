using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace do_playlist_api.Models;

public partial class Cancion
{
    public int Cancionid { get; set; }

    public string Titulo { get; set; } = null!;

    public string Artista { get; set; } = null!;

    public string? Album { get; set; }

    public string? Genero { get; set; }

    [Column(TypeName = "time")]
    public TimeOnly? Duracion { get; set; } = null;

    public virtual ICollection<Playlist> Playlists { get; set; } = new List<Playlist>();

}

public partial class CancionInput
{
    public int Cancionid { get; set; }
    public string Titulo { get; set; } = null!;
    public string Artista { get; set; } = null!;
    public string? Album { get; set; }
    public string? Genero { get; set; }
    public int Minutes { get; set; }
    public int Seconds { get; set; }

}


