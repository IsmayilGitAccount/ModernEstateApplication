using ModernEstateApplication.Models;

namespace ModernEstateApplication.ViewModels.Properties
{
    public class PropertyVM
    {
        public ICollection<GetPropertyVM> Property { get; set; }
        public ICollection<Category> Category { get; set; }
    }
}
