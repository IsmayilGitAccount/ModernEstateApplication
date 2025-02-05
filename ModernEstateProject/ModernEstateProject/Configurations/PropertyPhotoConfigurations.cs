using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModernEstateProject.Models;

namespace ModernEstateProject.Configurations
{
    public class PropertyPhotoConfigurations : IEntityTypeConfiguration<PropertyPhoto>
    {
        public void Configure(EntityTypeBuilder<PropertyPhoto> builder)
        {
            builder.Property(p => p.Photo).IsRequired().HasColumnType("char(200)");
            builder.HasIndex(x => x.Photo).IsUnique();
        }
    }
}
