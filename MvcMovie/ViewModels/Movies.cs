using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MvcMovie.Models
{
    public class MovieViewModel
    {
        public int Movie_ID { get; set; }

        [Display(Name = "Titre")]
        public string Title { get; set; }

        [Display(Name = "Année")]
        public int Year { get; set; }

        public string Genre { get; set; }

        [Display(Name = "Prix")]
        public decimal Price { get; set; }
    }

    public class MovieDisplayViewModel
    {
        public int Movie_ID { get; set; }

        [Display(Name = "Titre")]
        public string Title { get; set; }

        [Display(Name = "Date de sortie")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        public string Genre { get; set; }

        [Display(Name = "Prix")]
        public decimal Price { get; set; }

        [Display(Name = "Classement")]
        public RatingEnum Rating { get; set; }
    }

    public class MovieEditorViewModel
    {
        public int Movie_ID { get; set; }

        [Display(Name = "Titre")]
        [Required, StringLength(60, MinimumLength = 3)]
        public string Title { get; set; }

        [Display(Name = "Date de sortie")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime? ReleaseDate { get; set; }

        [Display(Name = "Genre")]
        public int Genre_ID { get; set; }
        public List<SelectListItem> Genres { get; set; }

        [Display(Name = "Prix")]
        [Range(1, 100), DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Display(Name = "Classement")]
        public RatingEnum Rating { get; set; }

        public Movie ToMovie()
        {
            var model = new Movie
            {
                Movie_ID = Movie_ID,
                Title = Title,
                ReleaseDate = ReleaseDate.GetValueOrDefault(),
                Genre_ID = Genre_ID,
                Price = Price,
                Rating = Rating
            };

            return model;
        }
    }
}
