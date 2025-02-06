using ModernEstate.Domain.Entities.Base;

namespace ModernEstate.Domain.Entities
{
    public class Types : BaseEntity
    {
        public string TypeName { get; set; }
        public ICollection<Property>? Properties { get; set; }
    }
}
