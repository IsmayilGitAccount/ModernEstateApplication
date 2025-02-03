using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModernEstateDemo.Models;

namespace ModernEstateDemo.Configurations
{
    public class AgentConfigurations : IEntityTypeConfiguration<Agent>
    {
        public void Configure(EntityTypeBuilder<Agent> builder)
        {
            builder.Property(a => a.FullName).IsRequired().HasColumnType("nvarchar(100)");
            builder.Property(a => a.PhoneNumber).IsRequired().HasColumnType("nvarchar(50)");
            builder.Property(a => a.Email).IsRequired().HasColumnType("nvarchar(255)");
            builder.Property(a => a.Address).IsRequired().HasColumnType("nvarchar(100)");
            builder.Property(a => a.Description).IsRequired().HasColumnType("nvarchar(200)");
            builder.Property(a => a.Photo).IsRequired().HasColumnType("nvarchar(100)");
            builder.Property(a => a.FacebookLink).IsRequired().HasColumnType("nvarchar(100)");
            builder.Property(a => a.XLink).IsRequired().HasColumnType("nvarchar(100)");
            builder.Property(a => a.LinkedinLink).IsRequired().HasColumnType("nvarchar(100)");
            builder.Property(a => a.InstagramLink).IsRequired().HasColumnType("nvarchar(100)");
            builder.HasOne(a => a.Agency).WithMany(ag => ag.Agents).HasForeignKey(a => a.AgencyId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
