using System.ComponentModel.DataAnnotations;

namespace ModernEstate.Application.ViewModels.Account
{
    public class ChangePasswordVM
    {
        [Required(ErrorMessage = "Email is required!")]
        [MinLength(5)]
        [MaxLength(255)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [DataType(DataType.Password)]
        [MaxLength(255)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm Password is required!")]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword))]
        [Display(Name = "Confirm New Password!")]
        public string ConfirmNewPassword { get; set; }
    }
}
