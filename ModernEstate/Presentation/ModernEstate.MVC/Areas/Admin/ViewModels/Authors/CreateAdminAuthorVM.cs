using System.ComponentModel.DataAnnotations;

namespace ModernEstate.MVC.Areas.Admin.ViewModels.Authors
{
    public class CreateAdminAuthorVM
    {
        [Required(ErrorMessage = "Please enter photo!")]
        public IFormFile Photo { get; set; }

        [Required(ErrorMessage = "Please enter author name!")]
        public string AuthorName { get; set; }

        [Required(ErrorMessage = "Please enter description!")]
        public string Description { get; set; }
    }
}
