﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Working.ViewModels
{
    public class LoginViewModel
    {
        [Required,DisplayName ("Username")]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
