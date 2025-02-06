using ModernEstate.Application.ViewModels.Paginations;
using ModernEstate.Domain.Entities;

namespace ModernEstate.Application.ViewModels.Agents
{
    public class AgentVM
    {
        public Agent Agent { get; set; }
        public List<Property> Properties { get; set; }
    }

}