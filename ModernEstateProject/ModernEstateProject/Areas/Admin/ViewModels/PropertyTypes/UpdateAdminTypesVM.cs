using System.ComponentModel.DataAnnotations;

namespace ModernEstateProject.Areas.Admin.ViewModels.Types
{
    public class UpdateAdminTypesVM
    {
        [Required(ErrorMessage = "Please enter type!")]
        public string TypesName { get; set; }
    }
}
