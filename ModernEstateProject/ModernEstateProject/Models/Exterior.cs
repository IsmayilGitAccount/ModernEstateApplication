namespace ModernEstateProject.Models
{
    public class Exterior : BaseEntity
    {
        public string ExteriorType { get; set; }
        public ICollection<Property>? Properties { get; set; }
    }
}
