using System.ComponentModel.DataAnnotations;

namespace ModernEstateProject.Areas.Admin.ViewModels.Categories
{
    public class UpdateAdminCategoryVM
    {
        [Required(ErrorMessage = "Please enter category name!")]
        public string CategoryName { get; set; }

        public IFormFile? Photo { get; set; }
    }
}
