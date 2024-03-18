using Application.Interfaces.IServices.Administration;
using Application.Services.Administration;
using Application.Services.Travellers;
using Application.Interfaces.IServices.Travellers;
namespace BookingAgency.Ioc
{
    public static class IocServices
    {
        public static IServiceCollection AddDependency(this IServiceCollection services)
        {
            services.AddTransient<IHotelServices, HotelServices>();
            services.AddTransient<IRoomServices, RoomServices>();
            services.AddTransient<ILoginServices, LoginServices>();
            services.AddTransient<ITravellersServices, TravellersServices>();

            return services;
        }
    }
}
