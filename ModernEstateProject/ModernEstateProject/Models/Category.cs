namespace ModernEstateProject.Models
{
    public class Category : BaseEntity
    {
        public string CategoryName { get; set; }
        public string CategoryPhoto { get; set; }
        public ICollection<Property>? Properties { get; set; }
    }
}
