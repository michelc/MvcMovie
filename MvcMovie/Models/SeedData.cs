using System;
using System.Collections.Generic;
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
                SeedGenres(context);
                SeedMovies(context);
                SeedDirectors(context);
            }
        }

        private static void SeedGenres(MvcMovieContext context)
        {
            if (context.Genres.Any())
            {
                return;
            }

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
        }

        private static void SeedMovies(MvcMovieContext context)
        {
            if (context.Movies.Any())
            {
                return;
            }

            var movies = new[] {
                    new Movie
                        {
                            Title = "When Harry Met Sally...",
                            ReleaseDate = DateTime.Parse("1989-7-14"),
                            Genre = context.Genres.FirstOrDefault(x => x.Title == "Romantic Comedy"),
                            Price = 7.99M,
                            Rating = RatingEnum.R
                        },
                    new Movie
                        {
                            Title = "Ghostbusters",
                            ReleaseDate = DateTime.Parse("1984-6-8"),
                            Genre = context.Genres.FirstOrDefault(x => x.Title == "Comedy"),
                            Price = 8.99M,
                            Rating = RatingEnum.G
                        },
                    new Movie
                        {
                            Title = "Ghostbusters II",
                            ReleaseDate = DateTime.Parse("1989-6-16"),
                            Genre = context.Genres.FirstOrDefault(x => x.Title == "Comedy"),
                            Price = 9.99M,
                            Rating = RatingEnum.G
                        },
                    new Movie
                        {
                            Title = "Rio Bravo",
                            ReleaseDate = DateTime.Parse("1959-4-4"),
                            Genre = context.Genres.FirstOrDefault(x => x.Title == "Western"),
                            Price = 3.99M,
                            Rating = RatingEnum.NA
                        }
                };
            context.Movies.AddRange(movies);
            context.SaveChanges();
        }

        private static void SeedDirectors(MvcMovieContext context)
        {
            if (context.Directors.Any())
            {
                return;
            }

            var directors = new[] {
                    new Director
                        {
                            Name = "Rob Reiner",
                            Movies = new List<Movie> { 
                                context.Movies.FirstOrDefault(x => x.Title.Contains("Harry")) 
                            }
                        },
                    new Director
                        {
                            Name = "Ivan Reitman",
                            Movies = context.Movies.Where(x => x.Title.StartsWith("Ghost")).ToList()
                        },
                    new Director
                        {
                            Name = "Howard Hawks",
                            Movies = new List<Movie> {
                                context.Movies.FirstOrDefault(x => x.Title == "Rio Bravo")
                            }
                        }
                };
            context.Directors.AddRange(directors);
            context.SaveChanges();
        }
    }
}
