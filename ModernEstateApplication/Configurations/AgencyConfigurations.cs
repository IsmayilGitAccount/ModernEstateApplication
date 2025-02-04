using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ModernEstateApplication.Models;

namespace ModernEstateApplication.Configurations
{
    public class AgencyConfigurations : IEntityTypeConfiguration<Agency>
    {
        public void Configure(EntityTypeBuilder<Agency> builder)
        {
            builder.Property(a => a.AgencyName).IsRequired().HasColumnType("nvarchar(100)");
            builder.Property(a => a.Description).IsRequired().HasColumnType("nvarchar(200)");
        }
    }
}
