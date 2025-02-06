using ModernEstate.Domain.Entities.Base;

namespace ModernEstate.Domain.Entities
{
    public class Agency : BaseEntity
    {
        public string AgencyName { get; set; }
        public string Description { get; set; }
        public ICollection<Agent> Agents { get; set; }
        public ICollection<Property> Properties { get; set; }
        public Agent Agent { get; set; }
    }
}
