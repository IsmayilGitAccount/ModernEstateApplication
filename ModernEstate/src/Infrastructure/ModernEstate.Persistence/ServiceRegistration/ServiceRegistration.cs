using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModernEstate.Application.Abstractions.Services;
using ModernEstate.Persistence.Data;
using ModernEstate.Persistence.Implementations.Services;

namespace ModernEstate.Persistence.ServiceRegistration
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(opt =>
            opt.UseSqlServer(configuration.GetConnectionString("Default"))
            );
            services.AddScoped<ILayoutService, LayoutService>();
            return services;
        }
    }
}
