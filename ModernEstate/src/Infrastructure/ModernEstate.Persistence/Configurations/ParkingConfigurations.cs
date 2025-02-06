using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Domain.Entities;

namespace ModernEstate.Persistence.Configurations
{
    public class ParkingConfigurations : IEntityTypeConfiguration<Parking>
    {
        public void Configure(EntityTypeBuilder<Parking> builder)
        {
            builder.Property(c => c.ParkingType).IsRequired().HasColumnType("nvarchar(100)");
        }
    }
}
