using System.ComponentModel.DataAnnotations;

namespace ModernEstateProject.Areas.Admin.ViewModels.Roofs
{
    public class CreateAdminRoofVM
    {
        [Required(ErrorMessage = "Please enter roof name!")]
        public string RoofType { get; set; }
    }
}
