using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace ModernEstate.Application.ViewModels.AdminSlides
{
    public class CreateAdminSlideVM
    {
        [Required(ErrorMessage = "Please enter photo!")]
        public IFormFile Photo { get; set; }

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
