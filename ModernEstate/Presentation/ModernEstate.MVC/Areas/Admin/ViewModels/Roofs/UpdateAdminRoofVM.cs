using System.ComponentModel.DataAnnotations;

namespace ModernEstate.Areas.Admin.ViewModels.Roofs
{
    public class UpdateAdminRoofVM
    {
        [Required(ErrorMessage = "Please enter roof name!")]
        public string RoofType { get; set; }
    }
}
