using System.ComponentModel.DataAnnotations;

namespace ModernEstate.Application.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Username or Email is required!")]
        [MinLength(5, ErrorMessage = "Username or email must contain at least 5 characters!")]
        [MaxLength(255)]
        public string UserNameorEmail { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [MaxLength(255)]
        public string Password { get; set; }
    }
}
