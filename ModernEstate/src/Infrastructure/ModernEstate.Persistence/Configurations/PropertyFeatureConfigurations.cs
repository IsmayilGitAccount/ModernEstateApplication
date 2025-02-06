using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModernEstate.Domain.Entities;

namespace ModernEstate.Persistence.Configurations
{
    public class PropertyFeatureConfigurations : IEntityTypeConfiguration<PropertyFeature>
    {
        public void Configure(EntityTypeBuilder<PropertyFeature> builder)
        {
            builder.HasKey(x => new { x.PropertyId, x.FeatureId });
        }
    }
}
