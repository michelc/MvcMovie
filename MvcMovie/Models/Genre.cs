using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models
{
    public class Genre
    {
        [Key]
        public int Genre_ID { get; set; }

        [StringLength(30, MinimumLength = 3)]
        [Required]
        public string Title { get; set; }

        public List<Movie> Movies { get; set; }
    }
}
