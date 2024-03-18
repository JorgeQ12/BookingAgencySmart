using Domain.Models;
using Application.Utilities;
using Application.DTOs;
using static Application.DTOs.HotelAndBooking;

namespace Application.Interfaces.IServices.Administration
{
    public interface IHotelServices 
    {
        Task<ResultResponse<List<HotelBookingDTO>>> GetAllBookings();
        Task<ResultResponse<List<HotelAndRoomDTO>>> GetAllHotel();
        Task<ResultResponse<Hotel>> NewHotel(HotelDTO hotel);
        Task<ResultResponse<string>> UpdateHotel(Guid idHotel, IDictionary<string, string> patchDoc);
        Task<ResultResponse<string>> DisabledOrEnabledHotel(Guid idHotel, bool Enabled);
    }
}
