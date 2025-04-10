namespace ReservationApi.Middlewares.Extensions
{
    public static class ExtensionsMiddleware
    {
        public static IApplicationBuilder UseRegisterLogMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RegisterLogMiddleware>();
        }
    }
}
