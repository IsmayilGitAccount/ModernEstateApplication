using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ModernEstateApplication.Models;

namespace ModernEstateApplication.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Agency> Agencies { get; set; }
        public DbSet<Agent> Agents { get; set; }
        public DbSet<PropertyPhotos> PropertyPhotos { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
