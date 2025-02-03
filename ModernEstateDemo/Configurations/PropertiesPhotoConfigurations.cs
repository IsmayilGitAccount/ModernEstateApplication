using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModernEstateDemo.Models;

namespace ModernEstateDemo.Configurations
{
    public class PropertiesPhotoConfigurations : IEntityTypeConfiguration<PropertiesPhoto>
    {
        public void Configure(EntityTypeBuilder<PropertiesPhoto> builder)
        {
            builder.Property(p => p.Photo).IsRequired().HasColumnType("char(200)");
            builder.HasIndex(x => x.Photo).IsUnique();
        }
    }
}
