using System.ComponentModel.DataAnnotations;

namespace ModernEstate.Application.ViewModels.Account
{
    public class ConfirmVerificationVM
    {
        public string Email { get; set; }

        [Required(ErrorMessage = "Verification code is required!")]
        [StringLength(7, MinimumLength = 7, ErrorMessage = "Verification code must be 7 digits!")]
        public string VerificationCode { get; set; }
    }
}
