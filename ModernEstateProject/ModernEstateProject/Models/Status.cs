namespace ModernEstateProject.Models
{
    public class Status : BaseEntity
    {
        public string StatusName { get; set; }
        public ICollection<Property>? Properties { get; set; }
    }
}
