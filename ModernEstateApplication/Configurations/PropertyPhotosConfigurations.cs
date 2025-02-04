using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ModernEstateApplication.Models;

namespace ModernEstateApplication.Configurations
{
    public class PropertyPhotosConfigurations : IEntityTypeConfiguration<PropertyPhotos>
    {
        public void Configure(EntityTypeBuilder<PropertyPhotos> builder)
        {
            builder.Property(p => p.Photo).IsRequired().HasColumnType("char(200)");
            builder.HasIndex(x => x.Photo).IsUnique();
        }
    }
}
