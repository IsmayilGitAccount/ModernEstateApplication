using Microsoft.EntityFrameworkCore;
using ModernEstateApplication.Data;

namespace ModernEstateApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>(opt =>
            opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"))
            );

            var app = builder.Build();

            app.UseStaticFiles();

            app.MapControllerRoute(
                name: "admin",
                pattern: "{Area:exists}/{Controller=Home}/{Action=Index}/{id?}"
                );

            app.MapControllerRoute(
                name: "default",
                pattern: "{Controller=Home}/{Action=Index}/{id?}"
                );
        }
    }
}
