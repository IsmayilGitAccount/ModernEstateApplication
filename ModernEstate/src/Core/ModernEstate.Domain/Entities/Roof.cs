using ModernEstate.Domain.Entities.Base;

namespace ModernEstate.Domain.Entities
{
    public class Roof : BaseEntity
    {
        public string RoofType { get; set; }
        public ICollection<Property>? Properties { get; set; }
    }
}
