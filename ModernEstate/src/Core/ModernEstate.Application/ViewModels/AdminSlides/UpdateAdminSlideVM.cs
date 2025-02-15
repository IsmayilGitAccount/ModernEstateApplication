using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ModernEstate.Application.ViewModels.AdminSlides
{
    public class UpdateAdminSlideVM
    {
        public IFormFile? Photo { get; set; }

        [Required(ErrorMessage = "Please enter order!")]
        public int Order { get; set; }

        [Required(ErrorMessage = "Please enter price!")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Please enter title!")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter description!")]
        public string Description { get; set; }
    }
}
