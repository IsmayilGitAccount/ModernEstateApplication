using Microsoft.AspNetCore.Http;

namespace ModernEstate.Application.ViewModels.AdminCategories
{
    public class CategoryItemVM
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string CategoryPhoto { get; set; }
        public int PropertyCount { get; set; }
    }
}
