using Microsoft.EntityFrameworkCore;
using MoviesApp.Models;

namespace MoviesApp.Data
{
    public class MoviesContext : DbContext
    {
        public MoviesContext (DbContextOptions<MoviesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Artist> Artists {get; set; }
        
        public virtual DbSet<MoviesArtist> MoviesArtists {get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");
            modelBuilder.Entity<MoviesArtist>(entity =>
            {
                entity.HasKey(e => new {e.MovieId, e.ArtistId});

                entity.ToTable("MoviesArtists");

                entity.HasOne(d => d.Artist)
                    .WithMany(p => p.MoviesArtists)
                    .HasForeignKey(d => d.ArtistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MoviesArtists_Artists");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.MoviesArtists)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MoviesArtists_Movies");
            });
        }
    }
}