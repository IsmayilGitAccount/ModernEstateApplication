using System.ComponentModel.DataAnnotations;

namespace ModernEstate.MVC.Areas.Admin.ViewModels.Exteriors
{
    public class UpdateAdminExteriorVM
    {
        [Required(ErrorMessage = "Please enter exterior name!")]
        public string ExteriorName { get; set; }
    }
}
