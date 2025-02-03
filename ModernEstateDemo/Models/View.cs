namespace ModernEstateDemo.Models
{
    public class View:BaseEntity
    {
        public string ViewType { get; set; }

        //Relational
        public ICollection<Property>? Properties { get; set; }
    }
}
