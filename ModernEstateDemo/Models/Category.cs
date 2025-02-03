namespace ModernEstateDemo.Models
{
    public class Category:BaseEntity
    {
        public string CategoryName { get; set; }
        public string CategoryPhoto { get; set; }

        //Relational
        public ICollection<Property>? Properties { get; set; }
    }
}
