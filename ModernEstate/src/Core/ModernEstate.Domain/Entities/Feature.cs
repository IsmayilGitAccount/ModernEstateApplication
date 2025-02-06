using ModernEstate.Domain.Entities.Base;

namespace ModernEstate.Domain.Entities
{
    public class Feature : BaseEntity
    {
        public string FeatureName { get; set; }
        public ICollection<PropertyFeature> PropertyFeatures { get; set; }
    }
}
