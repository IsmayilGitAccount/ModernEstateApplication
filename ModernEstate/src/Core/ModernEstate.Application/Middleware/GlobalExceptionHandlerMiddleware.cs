using System.Net.Http;
using Microsoft.AspNetCore.Http;

namespace ModernEstate.Application.Middleware
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception e)
            {
                var encodedMessage = Uri.EscapeDataString(e.Message);
                context.Response.Redirect($"/home/error?errorMessage={encodedMessage}");
            }
        }
    }
}
