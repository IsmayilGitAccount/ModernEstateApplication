using Microsoft.AspNetCore.Identity;
using ModernEstate.Domain.Entities.Account;
using ModernEstate.Persistence.Data;
using ModernEstate.Persistence.ServiceRegistration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistenceServices(builder.Configuration);

builder.Services.AddControllersWithViews();
builder.Services
    .AddIdentity<AppUser, IdentityRole>(

    opt =>
    {
        opt.Password.RequireNonAlphanumeric = false;
        opt.Password.RequiredLength = 8;
        opt.User.RequireUniqueEmail = true;
        opt.Lockout.AllowedForNewUsers = true;
        opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
        opt.Lockout.MaxFailedAccessAttempts = 3;
    }).AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>().AddDefaultUI();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "admin",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );
app.Run();
