using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModernEstateDemo.Models;

namespace ModernEstateDemo.Configurations
{
    public class ParkingConfigurations : IEntityTypeConfiguration<Parking>
    {
        public void Configure(EntityTypeBuilder<Parking> builder)
        {
            builder.Property(c => c.ParkingType).IsRequired().HasColumnType("nvarchar(100)");
        }
    }
}
