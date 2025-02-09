using System.ComponentModel.DataAnnotations;
using ModernEstate.Domain.Entities;

namespace ModernEstate.MVC.Areas.Admin.ViewModels.Properties
{
    public class UpdateAdminPropertyVM
    {
        public IFormFile? MainPhoto { get; set; }
        public ICollection<IFormFile>? AdditionalPhoto { get; set; }

        [Required(ErrorMessage = "Please enter Location!")]
        public string? Location { get; set; }

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
        public int? BuiltYear { get; set; }

        [Required(ErrorMessage = "Please enter size!")]
        public decimal? LotSize { get; set; }

        [Required(ErrorMessage = "Please enter district!")]
        public string? SchoolDistrict { get; set; }

        [Required(ErrorMessage = "Please enter count!")]
        public int? RoomCount { get; set; }

        [Required(ErrorMessage = "Please enter decription!")]

        public string? Description { get; set; }

        //Relational
        [Required(ErrorMessage = "Category is required!")]
        public int? CategoryId { get; set; }
        public ICollection<Category> Categories { get; set; }

        [Required(ErrorMessage = "Agent name is required!")]
        public int? AgentId { get; set; }
        public ICollection<Agent> Agents { get; set; }

        [Required(ErrorMessage = "Agency name is required!")]
        public int? AgencyId { get; set; }
        public ICollection<Agency> Agencies { get; set; }

        [Required(ErrorMessage = "Parking type is required!")]
        public int? ParkingId { get; set; }
        public ICollection<Parking> Parkings { get; set; }

        [Required(ErrorMessage = "Roof type is required!")]
        public int? RoofId { get; set; }
        public ICollection<Roof> Roofs { get; set; }

        [Required(ErrorMessage = "View type is required!")]
        public int? ViewId { get; set; }
        public ICollection<View> Views { get; set; }

        [Required(ErrorMessage = "Exterior type is required!")]
        public int? ExteriorId { get; set; }
        public ICollection<Exterior> Exteriors { get; set; }

        [Required(ErrorMessage = "Property status type is required!")]
        public int? StatusId { get; set; }
        public ICollection<Status> Status { get; set; }

        [Required(ErrorMessage = "Property type is required!")]
        public int? TypeId { get; set; }
        public ICollection<Types> Types { get; set; }
        public List<Feature>? Features { get; set; }
        public List<int>? FeatureIds { get; set; }
        public ICollection<PropertyPhoto>? PropertiesPhoto { get; set; }
        public List<int>? PhotoIds { get; set; }
    }
}
