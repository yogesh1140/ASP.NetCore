using IMDBDummy.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDBDummy.ViewModels
{
    public class MovieViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public int YearOfRelease { get; set; }
        public string Plot { get; set; }

        [Required]
        public string PosterUrl { get; set; }
        [Required]
        public ProducerViewModel Producer { get; set; }
        public ICollection<MovieActorViewModel> MovieActors { get; set; }
    }
}
