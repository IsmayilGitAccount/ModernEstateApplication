using ModernEstate.Domain.Entities.Base;

namespace ModernEstate.Domain.Entities
{
    public class Slide : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Photo { get; set; }
        public int Order { get; set; }
    }
}
