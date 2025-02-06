using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Domain.Entities;

namespace ModernEstate.Persistence.Configurations
{
    public class ExteriorConfigurations : IEntityTypeConfiguration<Exterior>
    {
        public void Configure(EntityTypeBuilder<Exterior> builder)
        {
            builder.Property(c => c.ExteriorType).IsRequired().HasColumnType("nvarchar(100)");
        }
    }
}
