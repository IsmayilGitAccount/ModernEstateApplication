using System.ComponentModel.DataAnnotations;

namespace ModernEstate.Areas.Admin.ViewModels.Types
{
    public class CreateAdminTypesVM
    {
        [Required(ErrorMessage = "Please enter type!")]
        public string TypesName { get; set; }
    }
}
