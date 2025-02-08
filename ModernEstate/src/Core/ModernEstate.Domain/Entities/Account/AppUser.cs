using Microsoft.AspNetCore.Identity;

namespace ModernEstate.Domain.Entities.Account
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsActive { get; set; }
    }
}
