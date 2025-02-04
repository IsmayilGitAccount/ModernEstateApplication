using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ModernEstateApplication.Models;

namespace ModernEstateApplication.Configurations
{
    public class ViewConfigurations : IEntityTypeConfiguration<View>
    {
        public void Configure(EntityTypeBuilder<View> builder)
        {
            builder.Property(c => c.ViewType).IsRequired().HasColumnType("nvarchar(100)");
        }
    }
}
