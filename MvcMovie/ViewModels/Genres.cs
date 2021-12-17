using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models
{
    public class GenreViewModel
    {
        public int Genre_ID { get; set; }

        [Display(Name = "Titre")]
        [Required, StringLength(30, MinimumLength = 3)]
        public string Title { get; set; }

        public Genre ToGenre()
        {
            var model = new Genre
            {
                Genre_ID = Genre_ID,
                Title = Title
            };

            return model;
        }
    }
}
