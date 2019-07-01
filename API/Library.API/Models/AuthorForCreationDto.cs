﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.API.Models
{
    public class AuthorForCreationDto
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        public DateTimeOffset DateOfBirth { get; set; }
        [Required]
        [MaxLength(50)]
        public string Genre { get; set; }
    }
}
