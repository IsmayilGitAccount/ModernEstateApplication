using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModernEstateDemo.Models;

namespace ModernEstateDemo.Configurations
{
    public class PropertyConfigurations : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder.Property(p => p.Location).IsRequired().HasColumnType("nvarchar(100)");
            builder.Property(p => p.Price).IsRequired();
            builder.Property(p => p.Area).IsRequired();
            builder.Property(p => p.BedroomCount).IsRequired().HasMaxLength(50);
            builder.Property(p => p.BathroomCount).IsRequired().HasMaxLength(50);
            builder.Property(p => p.GarageCount).IsRequired().HasMaxLength(50);
            builder.Property(p => p.LotSize).IsRequired();
            builder.Property(p => p.SchoolDistrict).IsRequired().HasColumnType("nvarchar(100)");
            builder.Property(p => p.RoomCount).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Description).IsRequired().HasColumnType("nvarchar(500)");
            builder.HasOne(p => p.Agent).WithMany(a => a.Properties).HasForeignKey(p => p.AgentId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(p => p.Agency) .WithMany(a => a.Properties).HasForeignKey(p => p.AgencyId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
