using Application.DTOs;
using Domain.Models;
using Application.Interfaces.IRepository.Common;

namespace Application.Interfaces.IRepository.Travellers
{
    public interface ITravellersRepository : IGenericRepository<Booking>
    {
        Task<List<HotelsByConditionDTO>> GetHotelByCondition(string city, DateTime entryDate, DateTime departureDate, int numberPeople);
        Task<ContactEmergency> AddContact(ContactEmergency contactEmergency);
        Task<List<Guest>> AddGuest(List<Guest> guest);
        Task<Room> GetRoom(Guid idRoom);
    }
}
