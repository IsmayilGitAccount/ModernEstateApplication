using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModernEstateProject.Models;

namespace ModernEstateProject.Configurations
{
    public class StatusConfigurations : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            builder.Property(c => c.StatusName).IsRequired().HasColumnType("nvarchar(100)");
        }
    }
}
