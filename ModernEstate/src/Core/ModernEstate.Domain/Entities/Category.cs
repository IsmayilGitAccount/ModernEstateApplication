using ModernEstate.Domain.Entities.Base;

namespace ModernEstate.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string CategoryName { get; set; }
        public string CategoryPhoto { get; set; }
        public ICollection<Property>? Properties { get; set; }
    }
}
