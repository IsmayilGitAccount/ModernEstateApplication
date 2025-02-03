using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModernEstateDemo.Models;

namespace ModernEstateDemo.Configurations
{
    public class ViewConfigurations : IEntityTypeConfiguration<View>
    {
        public void Configure(EntityTypeBuilder<View> builder)
        {
            builder.Property(c => c.ViewType).IsRequired().HasColumnType("nvarchar(100)");
        }
    }
}
