namespace ModernEstateApplication.Models
{
    public class PropertyPhotos : BaseEntity
    {
        public string Photo { get; set; }

        //Relational
        public int PropertyId { get; set; }
        public Property Property { get; set; }
    }
}
