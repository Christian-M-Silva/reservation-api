using ReservationApi.Interfaces.IRepositories;
using ReservationApi.Interfaces.IServices;
using ReservationApi.Repositories;
using ReservationApi.Services;

namespace ReservationApi.Settings
{
    public class ConfigureDependeciesInjection
    {
        public static void ConfigureDependecyInjection(IServiceCollection services)
        {
            //Services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<ILogService, LogService>();
            services.AddScoped<IReservationService, ReservationService>();



            //Repositories
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        }
    }
}
