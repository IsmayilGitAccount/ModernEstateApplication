using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModernEstateDemo.Models;

namespace ModernEstateDemo.Configurations
{
    public class ExteriorConfigurations : IEntityTypeConfiguration<Exterior>
    {
        public void Configure(EntityTypeBuilder<Exterior> builder)
        {
            builder.Property(c => c.ExteriorType).IsRequired().HasColumnType("nvarchar(100)");
        }
    }
}
