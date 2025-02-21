using Microsoft.AspNetCore.Builder;
using ModernEstate.Application.Middleware;

namespace ModernEstate.Application.ServiceRegistration
{
    public static class ServiceRegistration
    {
        public static IApplicationBuilder UseCustomMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
            return app;
        }
    }
}
