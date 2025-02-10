using ModernEstate.Domain.Entities;

namespace ModernEstate.Application.ViewModels.Services
{
    public class GetServiceVM
    {
        public List<Service> Service { get; set; }
        public Agency Agency { get; set; }
    }
}
