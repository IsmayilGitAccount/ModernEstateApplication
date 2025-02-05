using System.ComponentModel.DataAnnotations;

namespace ModernEstateProject.Areas.Admin.ViewModels.Exteriors
{
    public class UpdateAdminExteriorVM
    {
        [Required(ErrorMessage = "Please enter exterior name!")]
        public string ExteriorName { get; set; }
    }
}
