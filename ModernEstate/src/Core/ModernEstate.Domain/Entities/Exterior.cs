using ModernEstate.Domain.Entities.Base;

namespace ModernEstate.Domain.Entities
{
    public class Exterior : BaseEntity
    {
        public string ExteriorType { get; set; }
        public ICollection<Property>? Properties { get; set; }
    }
}
