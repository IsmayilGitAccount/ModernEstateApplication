using System.ComponentModel.DataAnnotations;

namespace ModernEstate.Application.ViewModels.AdminAssignments
{
    public class CreateAdminAssignmentVM
    {
        [Required(ErrorMessage = "Please enter title!")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter priority!")]
        public string Priority { get; set; }

        [Required(ErrorMessage = "Please enter status!")]
        public string Status { get; set; }

        [Required(ErrorMessage = "Please enter description!")]
        public string Description { get; set; }
    }
}
