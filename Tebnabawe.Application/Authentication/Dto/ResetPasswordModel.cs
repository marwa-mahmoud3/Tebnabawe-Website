using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tebnabawe.Application.Authentication
{
    public class ResetPasswordModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        [Required]
        public string ConfrimPassword { get; set; }
    }
}
