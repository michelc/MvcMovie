using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models
{
    public class Genre
    {
        [Key]
        public int Genre_ID { get; set; }

        [Required, StringLength(30)]
        public string Title { get; set; }

        public List<Movie> Movies { get; set; }
    }
}
