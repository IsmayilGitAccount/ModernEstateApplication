using System.ComponentModel.DataAnnotations;
using ModernEstate.Domain.Entities;

namespace ModernEstate.MVC.Areas.Admin.ViewModels.Posts
{
    public class CreateAgentServiceVM
    {
        [Required(ErrorMessage = "Please enter title!")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter photo!")]
        public IFormFile Photo { get; set; }

        [Required(ErrorMessage = "Please enter date&time!")]
        public DateTime PostedAt { get; set; }

        [Required(ErrorMessage = "Please enter agency name!")]
        public int? AgencyId { get; set; }
        public List<Agency>? Agency { get; set; }

        [Required(ErrorMessage = "Please enter author name!")]
        public int? AuthorId { get; set; }
        public List<Author>? Author { get; set; }

        [Required(ErrorMessage = "Please enter description!")]
        [MaxLength(1000)]
        public string Description { get; set; }
    }
}
