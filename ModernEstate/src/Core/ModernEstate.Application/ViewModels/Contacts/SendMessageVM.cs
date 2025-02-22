using System.ComponentModel.DataAnnotations;
using ModernEstate.Domain.Entities;

namespace ModernEstate.Application.ViewModels.Contacts
{
    public class SendMessageVM
    {
        public List<Contact> Messages { get; set; }

        [Required(ErrorMessage = "Please enter message!")]
        public string Message { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string UserId { get; set; }
        public string AdminId { get; set; }

    }
}
