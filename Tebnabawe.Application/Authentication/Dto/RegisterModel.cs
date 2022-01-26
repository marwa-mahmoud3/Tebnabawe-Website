using System.ComponentModel.DataAnnotations;

namespace Tebnabawe.Application.Authentication
{
    public class RegisterModel
    {
        public string UserName { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

    }
}