using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Models
{
    public class PointOfInterestForCreationDto
    {
        [Required(ErrorMessage = "You should provide a name value") , MaxLength(50, ErrorMessage ="Name can only have 50 characters")]
        public string Name { get; set; }
        [MaxLength(200, ErrorMessage = "Description can only have 200 characters")]
        public string Description { get; set; }
    }
}
