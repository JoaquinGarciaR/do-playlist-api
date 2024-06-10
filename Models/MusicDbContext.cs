using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace do_playlist_api.Models;

public partial class MusicDbContext : DbContext
{
    public MusicDbContext()
    {
    }

    public MusicDbContext(DbContextOptions<MusicDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cancion> Canciones { get; set; }

    public virtual DbSet<Playlist> Playlists { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cancion>(entity =>
        {
            entity.HasKey(e => e.Cancionid).HasName("cancion_pkey");

            entity.ToTable("cancion");

            entity.Property(e => e.Cancionid).HasColumnName("cancionid");
            entity.Property(e => e.Album)
                .HasMaxLength(100)
                .HasColumnName("album");
            entity.Property(e => e.Artista)
                .HasMaxLength(100)
                .HasColumnName("artista");
            entity.Property(e => e.Duracion).HasColumnName("duracion");
            entity.Property(e => e.Genero)
                .HasMaxLength(50)
                .HasColumnName("genero");
            entity.Property(e => e.Titulo)
                .HasMaxLength(100)
                .HasColumnName("titulo");
        });

        modelBuilder.Entity<Playlist>(entity =>
        {
            entity.HasKey(e => e.Playlistid).HasName("playlist_pkey");

            entity.ToTable("playlist");

            entity.Property(e => e.Playlistid).HasColumnName("playlistid");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");

            entity.HasMany(d => d.Canciones).WithMany(p => p.Playlists)
                .UsingEntity<Dictionary<string, object>>(
                    "Playlistcanciones",
                    r => r.HasOne<Cancion>().WithMany()
                        .HasForeignKey("Cancionid")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("playlistcanciones_cancionid_fkey"),
                    l => l.HasOne<Playlist>().WithMany()
                        .HasForeignKey("Playlistid")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("playlistcanciones_playlistid_fkey"),
                    j =>
                    {
                        j.HasKey("Playlistid", "Cancionid").HasName("playlistcanciones_pkey");
                        j.ToTable("playlistcanciones");
                        j.IndexerProperty<int>("Playlistid").HasColumnName("playlistid");
                        j.IndexerProperty<int>("Cancionid").HasColumnName("cancionid");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
