using System.ComponentModel.DataAnnotations;

namespace ModernEstate.MVC.Areas.Admin.ViewModels.Parkings
{
    public class CreateAdminParkingVM
    {
        [Required(ErrorMessage = "Please enter parking type!")]
        public string ParkingType { get; set; }
    }
}
