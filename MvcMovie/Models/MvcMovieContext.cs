using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MvcMovie.Models
{
    public class MvcMovieContext : DbContext
    {
        public MvcMovieContext(DbContextOptions<MvcMovieContext> options) : base(options) { }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Evite le ON CASCADE DELETE quand on suprime un genre
            modelBuilder.Entity<Genre>()
                        .HasMany(x => x.Movies)
                        .WithOne(x => x.Genre)
                        .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
