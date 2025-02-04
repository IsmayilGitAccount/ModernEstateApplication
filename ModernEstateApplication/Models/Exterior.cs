namespace ModernEstateApplication.Models
{
    public class Exterior : BaseEntity
    {
        public string ExteriorType { get; set; }

        //Relational
        public ICollection<Property>? Properties { get; set; }
    }
}
