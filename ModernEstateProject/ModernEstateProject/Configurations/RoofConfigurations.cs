using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModernEstateProject.Models;

namespace ModernEstateProject.Configurations
{
    public class RoofConfigurations : IEntityTypeConfiguration<Roof>
    {
        public void Configure(EntityTypeBuilder<Roof> builder)
        {
            builder.Property(c => c.RoofType).IsRequired().HasColumnType("nvarchar(100)");
        }
    }
}
