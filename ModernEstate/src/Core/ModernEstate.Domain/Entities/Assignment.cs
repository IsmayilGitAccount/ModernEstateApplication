using ModernEstate.Domain.Entities.Base;

namespace ModernEstate.Domain.Entities
{
    public class Assignment : BaseEntity
    {
        public string Title { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
    }
}
