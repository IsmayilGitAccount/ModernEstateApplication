using ModernEstate.Domain.Entities.Base;

namespace ModernEstate.Domain.Entities
{
    public class FAQ : BaseEntity
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public int AgencyId { get; set; }
        public Agency Agency { get; set; }
    }
}
