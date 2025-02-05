using ModernEstateProject.Models;

namespace ModernEstateProject.ViewModels.Properties
{
    public class PropertyVM
    {
        public ICollection<GetPropertyVM> Property { get; set; }
        public ICollection<Category> Category { get; set; }
        public ICollection<Status> Status { get; set; }
        public ICollection<Types> Types { get; set; }
    }
}
