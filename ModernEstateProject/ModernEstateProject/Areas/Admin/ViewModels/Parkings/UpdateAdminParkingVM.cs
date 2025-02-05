using System.ComponentModel.DataAnnotations;

namespace ModernEstateProject.Areas.Admin.ViewModels.Parkings
{
    public class UpdateAdminParkingVM
    {
        [Required(ErrorMessage = "Please enter parking type!")]
        public string ParkingType { get; set; }
    }
}
