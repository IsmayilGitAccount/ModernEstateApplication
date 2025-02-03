using ModernEstateDemo.Models;

namespace ModernEstateDemo.ViewModels
{
    public class SingleProductVM
    {
        public Property Property { get; set; }
        public ICollection<PropertiesPhoto> PropertiesPhoto { get; set; }
        public ICollection<Property> RecentlyProperties { get; set; }
    }
}
