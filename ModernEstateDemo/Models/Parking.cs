namespace ModernEstateDemo.Models
{
    public class Parking:BaseEntity
    {
        public string ParkingType { get; set; }

        //Relational
        public ICollection<Property>? Properties { get; set; }
    }
}
