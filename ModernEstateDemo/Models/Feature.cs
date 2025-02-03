namespace ModernEstateDemo.Models
{
    public class Feature:BaseEntity
    {
        public string FeatureName { get; set; }

        //Relational
        public ICollection<PropertyFeature> PropertyFeatures { get; set; }
    }
}
