using ModernEstate.Domain.Entities;

namespace ModernEstate.Application.ViewModels.Reviews
{
    public class CreateReviewVM
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public Property Property { get; set; }
        public ICollection<PropertyPhoto> PropertyPhotos { get; set; }
        public ICollection<Property> RecentlyProperties { get; set; }
    }
}
