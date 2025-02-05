using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ModernEstateProject.Models;

namespace ModernEstateProject.Data
{
    public class AppDbContext : DbContext
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
        public DbSet<Category> Categories { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<View> Views { get; set; }
        public DbSet<Exterior> Exteriors { get; set; }
        public DbSet<Roof> Roofs { get; set; }
        public DbSet<Parking> Parkings { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Types> Types { get; set; }
        public DbSet<PropertyPhoto> PropertiesPhotos { get; set; }
        public DbSet<PropertyFeature> PropertyFeatures { get; set; }
    }
}
