namespace ModernEstateProject.Models
{
    public class Feature : BaseEntity
    {
        public string FeatureName { get; set; }
        public ICollection<PropertyFeature> PropertyFeatures { get; set; }
    }
}
