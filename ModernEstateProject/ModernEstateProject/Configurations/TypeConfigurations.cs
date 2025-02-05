using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModernEstateProject.Models;

namespace ModernEstateProject.Configurations
{
    public class TypeConfigurations : IEntityTypeConfiguration<Types>
    {
        public void Configure(EntityTypeBuilder<Types> builder)
        {
            builder.Property(t=>t.TypeName).IsRequired().HasMaxLength(256);
        }
    }
}
