using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Domain.Entities;

namespace ModernEstate.Persistence.Configurations
{
    public class StatusConfigurations : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            builder.Property(c => c.StatusName).IsRequired().HasColumnType("nvarchar(100)");
        }
    }
}
