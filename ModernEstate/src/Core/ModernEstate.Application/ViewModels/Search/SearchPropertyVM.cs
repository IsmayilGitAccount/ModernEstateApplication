using ModernEstate.Application.ViewModels.Agents;
using ModernEstate.Application.ViewModels.Properties;
using ModernEstate.Domain.Entities;

namespace ModernEstate.Application.ViewModels.Search
{
    public class SearchPropertyVM
    {
        public PropertyVM PropertyData { get; set; } = new PropertyVM(); // PropertyVM-i içəridə saxlayır
        public List<Property> Properties { get; set; } = new List<Property>(); // Filtrlənmiş property-lər
    }

}
