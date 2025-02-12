using System.ComponentModel.DataAnnotations;

namespace ModernEstate.Application.ViewModels.AdminAgencies;
public class CreateAdminAgencyVM
{
    [Required(ErrorMessage = "Please enter Agency Name!")]
    public string AgencyName { get; set; }

    [Required(ErrorMessage = "Please enter Description!")]
    public string Description { get; set; }
}
