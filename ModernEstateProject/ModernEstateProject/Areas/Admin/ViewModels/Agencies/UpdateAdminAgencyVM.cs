using System.ComponentModel.DataAnnotations;

namespace ModernEstateProject.Areas.Admin.ViewModels.Agencies
{
    public class UpdateAdminAgencyVM
    {
        [Required(ErrorMessage = "Please enter Agency Name!")]
        public string AgencyName { get; set; }

        [Required(ErrorMessage = "Please enter Description!")]
        public string Description { get; set; }
    }
}
