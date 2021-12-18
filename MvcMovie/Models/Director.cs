using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models
{
    public class Director
    {
        [Key]
        public int Director_ID { get; set; }

        [Required, StringLength(60)]
        public string Name { get; set; }

        public ICollection<Movie> Movies { get; set; }
    }
}
