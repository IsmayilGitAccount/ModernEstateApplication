using System.ComponentModel.DataAnnotations;

namespace ModernEstate.Application.ViewModels.Account
{
    public class VerifyEmailVM
    {
        [Required(ErrorMessage = "Email is required!")]
        [MinLength(5, ErrorMessage = "Email must contain at least 5 characters!")]
        [MaxLength(255)]
        public string Email { get; set; }
    }
}
