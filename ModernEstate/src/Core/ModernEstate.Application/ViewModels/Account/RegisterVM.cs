using System.ComponentModel.DataAnnotations;

namespace ModernEstate.Application.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Name is required!")]
        [MinLength(3)]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname is required!")]
        [MinLength(3)]
        [MaxLength(100)]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Username is required!")]
        [MinLength(5)]
        [MaxLength(255)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required!")]
        [MinLength(5)]
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
