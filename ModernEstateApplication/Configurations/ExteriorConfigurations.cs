using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ModernEstateApplication.Models;

namespace ModernEstateApplication.Configurations
{
    public class ExteriorConfigurations : IEntityTypeConfiguration<Exterior>
    {
        public void Configure(EntityTypeBuilder<Exterior> builder)
        {
            builder.Property(c => c.ExteriorType).IsRequired().HasColumnType("nvarchar(100)");
        }
    }
}
