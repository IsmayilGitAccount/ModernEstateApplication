using System.ComponentModel.DataAnnotations;
using ModernEstateProject.Models;

namespace ModernEstateProject.Areas.Admin.ViewModels.Agents
{
    public class CreateAdminAgentVM
    {
        [Required(ErrorMessage = "Please enter name!")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Please enter number!")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please enter email!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter address!")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please enter description!")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please choose file!")]
        public IFormFile Photo { get; set; }

        [Required(ErrorMessage = "Please enter facebook link!")]
        public string FacebookLink { get; set; }

        [Required(ErrorMessage = "Please enter instagram link!")]
        public string InstagramLink { get; set; }

        [Required(ErrorMessage = "Please enter twitter link!")]
        public string XLink { get; set; }

        [Required(ErrorMessage = "Please enter linkedin link!")]
        public string LinkedinLink { get; set; }
        public int AgencyId { get; set; }
        public List<Agency>? Agencies { get; set; }
    }
}
