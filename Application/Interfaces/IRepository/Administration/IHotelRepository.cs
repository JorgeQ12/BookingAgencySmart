using Application.DTOs;
using Domain.Models;
using Application.Interfaces.IRepository.Common;
using static Application.DTOs.HotelAndBooking;

namespace Application.Interfaces.IRepository.Administration
{
    public interface IHotelRepository : IGenericRepository<Hotel>
    {
        Task<List<HotelAndRoomDTO>> GetAllHotel();
        Task<List<HotelBookingDTO>> GetAllBookings();
    }
}
