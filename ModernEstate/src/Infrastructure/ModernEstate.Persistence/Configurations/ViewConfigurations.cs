using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Domain.Entities;

namespace ModernEstate.Persistence.Configurations
{
    public class ViewConfigurations : IEntityTypeConfiguration<View>
    {
        public void Configure(EntityTypeBuilder<View> builder)
        {
            builder.Property(c => c.ViewType).IsRequired().HasColumnType("nvarchar(100)");
        }
    }
}
