using ModernEstate.Domain.Entities;

namespace ModernEstate.Application.ViewModels.Agencies
{
    public class AgencyVM
    {
        public Agency Agency { get; set; }
        public List<Agent> Agent { get; set; }
    }
}
