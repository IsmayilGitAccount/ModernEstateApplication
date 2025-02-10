using ModernEstate.Domain.Entities.Base;

namespace ModernEstate.Domain.Entities
{
    public class Service : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public int AgencyId { get; set; }
        public Agency Agency { get; set; }
    }
}
