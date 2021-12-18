using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models
{
    public class DirectorViewModel
    {
        public int Director_ID { get; set; }

        [Display(Name = "Nom")]
        [Required, StringLength(60, MinimumLength = 3)]
        public string Name { get; set; }

        public List<IdCaption> Movies { get; set; }

        public Director ToDirector()
        {
            var model = new Director
            {
                Director_ID = Director_ID,
                Name = Name
            };

            return model;
        }
    }
}
