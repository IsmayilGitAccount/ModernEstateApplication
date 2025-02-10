using System.ComponentModel.DataAnnotations;
using ModernEstate.Domain.Entities;

namespace ModernEstate.MVC.Areas.Admin.ViewModels.FAQs
{
    public class UpdateAdminFAQVM
    {
        [Required(ErrorMessage = "Please enter question!")]
        public string Question { get; set; }

        [Required(ErrorMessage = "Please enter answer!")]
        public string Answer { get; set; }

        [Required(ErrorMessage = "Please enter agency name!")]
        public int? AgencyId { get; set; }
        public List<Agency> Agency { get; set; }
    }
}
