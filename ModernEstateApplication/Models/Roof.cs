namespace ModernEstateApplication.Models
{
    public class Roof : BaseEntity
    {
        public string RoofType { get; set; }

        //Relational
        public ICollection<Property>? Properties { get; set; }
    }
}
