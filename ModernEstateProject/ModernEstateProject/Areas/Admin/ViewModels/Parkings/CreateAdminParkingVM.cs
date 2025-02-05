using System.ComponentModel.DataAnnotations;

namespace ModernEstateProject.Areas.Admin.ViewModels.Parkings
{
    public class CreateAdminParkingVM
    {
        [Required(ErrorMessage = "Please enter parking type!")]
        public string ParkingType { get; set; }
    }
}
