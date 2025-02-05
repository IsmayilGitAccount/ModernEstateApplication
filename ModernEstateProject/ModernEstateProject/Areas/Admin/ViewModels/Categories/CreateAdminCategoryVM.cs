using System.ComponentModel.DataAnnotations;
using ModernEstateProject.Models;

namespace ModernEstateProject.Areas.Admin.ViewModels.Categories
{
    public class CreateAdminCategoryVM
    {
        [Required(ErrorMessage = "Please enter category name!")]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "Please choose file!")]
        public IFormFile Photo { get; set; }
    }
}
