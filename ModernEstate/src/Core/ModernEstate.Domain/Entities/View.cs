using ModernEstate.Domain.Entities.Base;

namespace ModernEstate.Domain.Entities
{
    public class View : BaseEntity
    {
        public string ViewType { get; set; }
        public ICollection<Property>? Properties { get; set; }
    }
}
