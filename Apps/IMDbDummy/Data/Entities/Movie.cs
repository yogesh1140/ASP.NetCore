using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDBDummy.Data.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int YearOfRelease { get; set; }
        public string Plot { get; set; }

        public string PosterUrl { get; set; }
        public Producer Producer { get; set; }
        public ICollection<MovieActor> MovieActors { get; set; } 
    }
}
