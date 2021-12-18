using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MvcMovie.Models
{
    public class MvcMovieContext : DbContext
    {
        public MvcMovieContext(DbContextOptions<MvcMovieContext> options) : base(options) { }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Director> Directors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Evite le ON CASCADE DELETE quand on suprime un genre
            modelBuilder.Entity<Genre>()
                        .HasMany(x => x.Movies)
                        .WithOne(x => x.Genre)
                        .OnDelete(DeleteBehavior.Restrict);

            // EF génère par défaut DirectorMovie(DirectorsDirector_ID, MoviesMovie_ID)
            // que je remplace par Directors_Movies(Director_ID, Movie_ID)
            modelBuilder.Entity<Movie>()
                        .HasMany(left => left.Directors)
                        .WithMany(right => right.Movies)
                        .UsingEntity<Dictionary<string, object>>(
                            "Directors_Movies",
                            j => j
                                .HasOne<Director>()
                                .WithMany()
                                .HasForeignKey("Director_ID"),
                            j => j
                                .HasOne<Movie>()
                                .WithMany()
                                .HasForeignKey("Movie_ID")
                        );
        }
    }
}
