using ModernEstate.Domain.Entities.Base;

namespace ModernEstate.Domain.Entities
{
    public class Property : BaseEntity
    {
        public string Location { get; set; }
        public decimal Price { get; set; }
        public decimal Area { get; set; }
        public int BedroomCount { get; set; }
        public int BathroomCount { get; set; }
        public int GarageCount { get; set; }
        public int? BuiltYear { get; set; }
        public decimal LotSize { get; set; }
        public string SchoolDistrict { get; set; }
        public int RoomCount { get; set; }
        public string Description { get; set; }

        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public int? AgentId { get; set; }
        public Agent Agent { get; set; }
        public int? AgencyId { get; set; }
        public Agency Agency { get; set; }
        public ICollection<PropertyPhoto> PropertyPhotos { get; set; }
        public List<PropertyFeature> PropertyFeatures { get; set; }
        public int? ParkingId { get; set; }
        public Parking Parking { get; set; }
        public int? RoofId { get; set; }
        public Roof Roof { get; set; }
        public int? ViewId { get; set; }
        public View View { get; set; }
        public int? ExteriorId { get; set; }
        public Exterior Exterior { get; set; }
        public int? StatusId { get; set; }
        public Status Status { get; set; }
        public int? TypeId { get; set; }
        public Types Type { get; set; }
    }
}
