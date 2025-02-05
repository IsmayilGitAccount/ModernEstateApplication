namespace ModernEstateProject.Models
{
    public class View : BaseEntity
    {
        public string ViewType { get; set; }
        public ICollection<Property>? Properties { get; set; }
    }
}
