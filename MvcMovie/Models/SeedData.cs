using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MvcMovie.Models
{
    public class SeedData
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();

            using (var context = new MvcMovieContext(scope.ServiceProvider.GetRequiredService<DbContextOptions<MvcMovieContext>>()))
            {
                // Rien à faire si la base de données contient déjà des données
                if (context.Movies.Any())
                {
                    return;
                }

                // Alimentation de la table des genres
                var genres = new[] {
                    new Genre
                        {
                            Title = "Romantic Comedy"
                        },
                    new Genre
                        {
                            Title = "Comedy"
                        },
                    new Genre
                        {
                            Title = "Western"
                        }
                };
                context.Genres.AddRange(genres);
                context.SaveChanges();

                // Alimentation de la table des films
                var movies = new[] {
                    new Movie
                        {
                            Title = "When Harry Met Sally",
                            ReleaseDate = DateTime.Parse("1989-2-12"),
                            Genre = context.Genres.FirstOrDefault(x => x.Title == "Romantic Comedy"),
                            Price = 7.99M,
                            Rating = RatingEnum.R
                        },
                    new Movie
                        {
                            Title = "Ghostbusters",
                            ReleaseDate = DateTime.Parse("1984-3-13"),
                            Genre = context.Genres.FirstOrDefault(x => x.Title == "Comedy"),
                            Price = 8.99M,
                            Rating = RatingEnum.G
                        },
                    new Movie
                        {
                            Title = "Ghostbusters 2",
                            ReleaseDate = DateTime.Parse("1986-2-23"),
                            Genre = context.Genres.FirstOrDefault(x => x.Title == "Comedy"),
                            Price = 9.99M,
                            Rating = RatingEnum.G
                        },
                    new Movie
                        {
                            Title = "Rio Bravo",
                            ReleaseDate = DateTime.Parse("1959-4-15"),
                            Genre = context.Genres.FirstOrDefault(x => x.Title == "Western"),
                            Price = 3.99M,
                            Rating = RatingEnum.NA
                        }
                };
                context.Movies.AddRange(movies);

                // Persistance des données dans la base de données
                context.SaveChanges();
            }
        }
    }
}
