using System.ComponentModel.DataAnnotations;

namespace ModernEstate.MVC.Areas.Admin.ViewModels.Parkings
{
    public class UpdateAdminParkingVM
    {
        [Required(ErrorMessage = "Please enter parking type!")]
        public string ParkingType { get; set; }
    }
}
