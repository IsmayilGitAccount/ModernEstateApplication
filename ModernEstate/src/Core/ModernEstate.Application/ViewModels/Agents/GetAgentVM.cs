using ModernEstate.Domain.Entities;

namespace ModernEstate.Application.ViewModels.Agents
{
    public class GetAgentVM
    {
        public string FullName { get; set; }
        public string Photo { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public Property Property { get; set; }
    }
}
