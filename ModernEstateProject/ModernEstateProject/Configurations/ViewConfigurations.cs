using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModernEstateProject.Models;

namespace ModernEstateProject.Configurations
{
    public class ViewConfigurations : IEntityTypeConfiguration<View>
    {
        public void Configure(EntityTypeBuilder<View> builder)
        {
            builder.Property(c => c.ViewType).IsRequired().HasColumnType("nvarchar(100)");
        }
    }
}
