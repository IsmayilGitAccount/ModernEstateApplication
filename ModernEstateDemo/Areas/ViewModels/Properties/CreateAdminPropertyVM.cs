using System.ComponentModel.DataAnnotations;
using ModernEstateDemo.Models;

namespace ModernEstateDemo.Areas.ViewModels.Properties
{
    public class CreateAdminPropertyVM
    {
        [Required(ErrorMessage ="Please enter Photo!")]
        public IFormFile MainPhoto { get; set; }
        public ICollection<IFormFile>? AdditionalPhoto { get; set; }

        [Required(ErrorMessage = "Please enter Location!")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Please enter Price!")]
        public decimal? Price { get; set; }

        [Required(ErrorMessage = "Please enter Area!")]

        public decimal? Area { get; set; }

        [Required(ErrorMessage = "Please enter count!")]

        public int? BedroomCount { get; set; }

        [Required(ErrorMessage = "Please enter count!")]

        public int? BathroomCount { get; set; }

        [Required(ErrorMessage = "Please enter count!")]

        public int? GarageCount { get; set; }

        [Required(ErrorMessage = "Please enter date!")]
        public DateTime BuiltYear { get; set; }

        [Required(ErrorMessage = "Please enter size!")]
        public decimal? LotSize { get; set; }

        [Required(ErrorMessage = "Please enter district!")]
        public string SchoolDistrict { get; set; }

        [Required(ErrorMessage = "Please enter count!")]
        public int? RoomCount { get; set; }

        [Required(ErrorMessage = "Please enter decription!")]

        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter status!")]
        public string PropertyStatus { get; set; }

        [Required(ErrorMessage = "Please enter property type!")]
        public string PropertyType { get; set; }

        //Relational
        public int CategoryId { get; set; }
        public ICollection<Category> Categories { get; set; }
        public int AgentId { get; set; }
        public ICollection<Agent> Agents { get; set; }
        public int AgencyId { get; set; }
        public ICollection<Agency> Agencies { get; set; }
        public int ParkingId { get; set; }
        public ICollection<Parking> Parkings { get; set; }
        public int RoofId { get; set; }
        public ICollection<Roof> Roofs { get; set; }
        public int ViewId { get; set; }
        public ICollection<View> Views { get; set; }
        public int ExteriorId { get; set; }
        public ICollection<Exterior> Exteriors { get; set; }
        public List<Feature>? Features { get; set; }
        public ICollection<int>? FeatureIds { get; set; }
    }
}
