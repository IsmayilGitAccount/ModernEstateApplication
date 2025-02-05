using System.ComponentModel.DataAnnotations;

namespace ModernEstateProject.Areas.Admin.ViewModels.Views
{
    public class CreateAdminViewVM
    {
        [Required(ErrorMessage = "Please enter view name!")]
        public string ViewType { get; set; }
    }
}
