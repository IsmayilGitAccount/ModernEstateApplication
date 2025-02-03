using ModernEstateDemo.Models;

namespace ModernEstateDemo.ViewModels
{
    public class PropertyVM
    {
        public ICollection<GetPropertyVM> Property { get; set; }
        public ICollection<Category> Category { get; set; }
    }
}
