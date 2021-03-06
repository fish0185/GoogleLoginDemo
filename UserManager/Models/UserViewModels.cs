﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserManager.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CreateModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class LoginModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
    }
}