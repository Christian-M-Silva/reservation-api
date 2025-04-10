using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using ReservationApi.Interfaces.IServices;
using ReservationApi.Models.Entities;
using System.Threading.Tasks;

namespace ReservationApi.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class RegisterLogMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task Invoke(HttpContext httpContext, ILogService logService)
        {
            var route = httpContext.Request.Path.Value?.ToLower();

            if (route != null && (!route.EndsWith("/login") || !route.EndsWith("/register")))
            {
                await logService.RegisterLog(new LogEntity()
                {
                    LogTime = DateTime.UtcNow,
                    Method = httpContext.Request.Method,
                    Route = route
                });
            }
            await _next(httpContext);
            await logService.RegisterLog(new LogEntity()
            {
                LogTime = DateTime.UtcNow,
                Method = httpContext.Request.Method,
                Route = route ?? "Rota não especificada"
            });
        }
    }
}
