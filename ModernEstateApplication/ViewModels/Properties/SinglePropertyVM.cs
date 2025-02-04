using ModernEstateApplication.Models;

namespace ModernEstateApplication.ViewModels.Properties
{
    public class SinglePropertyVM
    {
        public Property Property { get; set; }
        public ICollection<PropertyPhotos> PropertyPhotos { get; set; }
        public ICollection<Property> RecentlyProperties { get; set; }
    }
}
