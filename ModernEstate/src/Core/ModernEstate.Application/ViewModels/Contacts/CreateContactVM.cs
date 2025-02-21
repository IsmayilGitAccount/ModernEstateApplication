using System.ComponentModel.DataAnnotations;

namespace ModernEstate.Application.ViewModels.Contacts
{
    public class CreateContactVM
    {
        public string UserId { get; set; }

        [Required(ErrorMessage = "Please enter name!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter email!")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Please enter phone number!")]
        public string Phone { get; set; }
        
        [Required(ErrorMessage = "Please enter message!")]
        public string Message { get; set; }
    }
}
