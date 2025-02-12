using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace ModernEstate.MVC.Areas.Admin.ViewModels.Authors
{
    public class UpdateAdminAuthorVM
    {
        public IFormFile? Photo { get; set; }

        [Required(ErrorMessage = "Please enter author name!")]
        public string AuthorName { get; set; }

        [Required(ErrorMessage = "Please enter description!")]
        public string Description { get; set; }
    }
}
