using System.ComponentModel.DataAnnotations;

namespace ModernEstate.MVC.Areas.Admin.ViewModels.Categories
{
    public class CreateAdminCategoryVM
    {
        [Required(ErrorMessage = "Please enter category name!")]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "Please choose file!")]
        public IFormFile Photo { get; set; }
    }
}
