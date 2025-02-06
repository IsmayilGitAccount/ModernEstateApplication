using System.ComponentModel.DataAnnotations;

namespace ModernEstate.MVC.Areas.Admin.ViewModels.Agencies
{
    public class CreateAdminAgencyVM
    {
        [Required(ErrorMessage = "Please enter Agency Name!")]
        public string AgencyName { get; set; }

        [Required(ErrorMessage = "Please enter Description!")]
        public string Description { get; set; }
    }
}
