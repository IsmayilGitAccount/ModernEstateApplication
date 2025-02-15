using System.ComponentModel.DataAnnotations;

namespace ModernEstate.Application.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Name is required!")]
        [MinLength(3, ErrorMessage = "Name must contain at least 3 characters!")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname is required!")]
        [MinLength(3, ErrorMessage = "Surname must contain at least 3 characters!")]
        [MaxLength(100)]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Username is required!")]
        [MinLength(5, ErrorMessage = "Username must contain at least 5 characters!")]
        [MaxLength(255)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required!")]
        [MinLength(5, ErrorMessage = "Email must contain at least 5 characters!")]
        [MaxLength(255)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [DataType(DataType.Password)]
        [MaxLength(255)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required!")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
