namespace ModernEstateProject.Models
{
    public class Roof : BaseEntity
    {
        public string RoofType { get; set; }
        public ICollection<Property>? Properties { get; set; }
    }
}
