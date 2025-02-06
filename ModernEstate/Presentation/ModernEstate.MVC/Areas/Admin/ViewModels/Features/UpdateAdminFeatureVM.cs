using System.ComponentModel.DataAnnotations;

namespace ModernEstate.MVC.Areas.Admin.ViewModels.Features
{
    public class UpdateAdminFeatureVM
    {
        [Required(ErrorMessage = "Please enter feature!")]
        public string FeatureName { get; set; }
    }
}
