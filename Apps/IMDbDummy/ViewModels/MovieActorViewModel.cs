using IMDBDummy.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDBDummy.ViewModels
{
    public class MovieActorViewModel
    {
        
        public int ActorId { get; set; }
        public ActorViewModel Actor { get; set; }
        
        //public int MovieId { get; set; }


    }
}
