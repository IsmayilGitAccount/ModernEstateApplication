using ModernEstate.Domain.Entities;

namespace ModernEstate.Application.ViewModels.Agents
{
    public class AgentVM
    {
        public Agent Agent { get; set; }
        public List<Property> Properties { get; set; }
        public double TotalPage { get; set; }
        public int CurrentPage { get; set; }

    }
}