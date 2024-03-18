using Application.Interfaces.IRepository.Administration;
using Application.Interfaces.IRepository.Travellers;
using Infraestructure.Repository.Administration;

namespace BookingAgency.Ioc
{
    public static class IocRepository
    {
        public static IServiceCollection AddDependency(this IServiceCollection services)
        {
            services.AddTransient<IHotelRepository, HotelRepository>();
            services.AddTransient<IRoomRepository, RoomRepository>();
            services.AddTransient<ILoginRepository, LoginRepository>();
            services.AddTransient<ITravellersRepository, TravellersRepository>();

            return services;
        }
    }
}
