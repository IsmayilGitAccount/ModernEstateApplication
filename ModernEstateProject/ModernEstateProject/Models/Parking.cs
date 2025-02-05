namespace ModernEstateProject.Models
{
    public class Parking : BaseEntity
    {
        public string ParkingType { get; set; }
        public ICollection<Property>? Properties { get; set; }
    }
}
