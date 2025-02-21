using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Domain.Entities;
using ModernEstate.Domain.Entities.Account;

namespace ModernEstate.Persistence.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Agency> Agencies { get; set; }
        public DbSet<Agent> Agents { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<View> Views { get; set; }
        public DbSet<Exterior> Exteriors { get; set; }
        public DbSet<Roof> Roofs { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Parking> Parkings { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Types> Types { get; set; }
        public DbSet<Slide> Slides { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<FAQ> FAQs { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Wishlist> Wishlist { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<PropertyPhoto> PropertiesPhotos { get; set; }
        public DbSet<PropertyFeature> PropertyFeatures { get; set; }
    }
}
