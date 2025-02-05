using Microsoft.EntityFrameworkCore;
using ModernEstateProject.Data;
using ModernEstateProject.Services.Implementations;
using ModernEstateProject.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(opt =>
opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"))
);
builder.Services.AddScoped<ILayoutService, LayoutService>();
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
app.Run();
