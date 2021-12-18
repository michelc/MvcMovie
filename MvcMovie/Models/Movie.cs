using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMovie.Models
{
    public class Movie
    {
        [Key]
        public int Movie_ID { get; set; }

        [Required, StringLength(60)]
        public string Title { get; set; }

        public DateTime ReleaseDate { get; set; }

        public int Genre_ID { get; set; }
        [ForeignKey("Genre_ID")]
        public virtual Genre Genre { get; set; }

        [Column(TypeName = "decimal(18, 2)")] 
        public decimal Price { get; set; }

        [Required]
        public RatingEnum Rating { get; set; }
    }

    public enum RatingEnum
    {
        [Display(Name = "Non renseigné")]
        NA,
        [Display(Name = "Tout public")]
        G,
        [Display(Name = "Contrôle parental")]
        PG,
        [Display(Name = "Interdit moins 13 ans")]
        PG_13,
        [Display(Name = "Mineur accompagné")]
        R,
        [Display(Name = "Interdit moins 18 ans")]
        NC_17
    }
}
