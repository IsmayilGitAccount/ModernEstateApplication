using System.ComponentModel.DataAnnotations;

namespace ModernEstateProject.Areas.Admin.ViewModels.Status
{
    public class UpdateAdminStatusVM
    {
        [Required(ErrorMessage = "Please enter status!")]
        public string StatusName { get; set; }
    }
}
