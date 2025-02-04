namespace ModernEstateApplication.Models
{
    public class PropertyFeature
    {
        //public bool IsDeleted { get; set; }
        public int PropertyId { get; set; }
        public int FeatureId { get; set; }
        public Property Property { get; set; }
        public Feature Feature { get; set; }
    }
}
