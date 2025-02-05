using ModernEstateProject.Models;

namespace ModernEstateProject.ViewModels.Agents
{
    public class AgentVM
    {
        public Agent Agent { get; set; }
        public List<Property> Properties { get; set; }
    }
}
