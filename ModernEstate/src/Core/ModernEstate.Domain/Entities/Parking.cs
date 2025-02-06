using ModernEstate.Domain.Entities.Base;

namespace ModernEstate.Domain.Entities
{
    public class Parking : BaseEntity
    {
        public string ParkingType { get; set; }
        public ICollection<Property>? Properties { get; set; }
    }
}
