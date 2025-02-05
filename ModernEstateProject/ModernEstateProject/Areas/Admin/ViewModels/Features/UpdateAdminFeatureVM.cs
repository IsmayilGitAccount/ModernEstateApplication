using System.ComponentModel.DataAnnotations;

namespace ModernEstateProject.Areas.Admin.ViewModels.Features
{
    public class UpdateAdminFeatureVM
    {
        [Required(ErrorMessage = "Please enter feature!")]
        public string FeatureName { get; set; }
    }
}
