namespace ModernEstateProject.Models
{
    public class Types : BaseEntity
    {
        public string TypeName { get; set; }
        public ICollection<Property>? Properties { get; set; }
    }
}
