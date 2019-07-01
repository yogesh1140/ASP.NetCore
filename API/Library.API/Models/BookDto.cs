using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.API.Models
{
    public class BookDto : LinkedResourceBaseDto
    {

        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
        Guid AuthtorId { get; set; }

    }
}
