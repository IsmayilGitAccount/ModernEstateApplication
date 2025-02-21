using ModernEstate.Domain.Entities.Account;
using ModernEstate.Domain.Entities.Base;
using ModernEstate.Domain.Enums;

namespace ModernEstate.Domain.Entities
{
    public class Contact : BaseEntity
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public AppUser User { get; set; }
        public string Role { get; set; }
    }
}
