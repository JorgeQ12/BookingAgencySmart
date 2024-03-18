using Application.DTOs;
using Application.Utilities;
using Domain.Models;

namespace Application.Interfaces.IServices.Travellers
{
    public interface ITravellersServices
    {
        Task<ResultResponse<List<HotelsByConditionDTO>>> GetHotelByCondition(DateTime entryDate, DateTime departureDate, int numberPeople, string city);
        Task<ResultResponse<string>> NewBooking(BookingDTO booking);
    }
}
