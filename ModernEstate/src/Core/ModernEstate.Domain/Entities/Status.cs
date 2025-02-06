using ModernEstate.Domain.Entities.Base;

namespace ModernEstate.Domain.Entities
{
    public class Status : BaseEntity
    {
        public string StatusName { get; set; }
        public ICollection<Property>? Properties { get; set; }
    }
}
