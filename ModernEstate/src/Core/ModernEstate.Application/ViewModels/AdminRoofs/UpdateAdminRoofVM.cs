using System.ComponentModel.DataAnnotations;

namespace ModernEstate.Application.ViewModels.AdminRoofs
{
    public class UpdateAdminRoofVM
    {
        [Required(ErrorMessage = "Please enter roof name!")]
        public string RoofType { get; set; }
    }
}
