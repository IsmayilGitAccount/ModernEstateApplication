using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ModernEstateApplication.Models;

namespace ModernEstateApplication.Configurations
{
    public class FeatureConfigurations : IEntityTypeConfiguration<Feature>
    {
        public void Configure(EntityTypeBuilder<Feature> builder)
        {
            builder.Property(c => c.FeatureName).IsRequired().HasColumnType("nvarchar(100)");
        }
    }
}
