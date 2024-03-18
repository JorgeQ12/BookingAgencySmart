using Application.DTOs;
using Application.Utilities;
using Application.Interfaces.IServices.Travellers;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;

namespace BookingAgency.Controllers.Travellers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TravellersController : ControllerBase
    {
        private readonly ITravellersServices _travellersServices;

        public TravellersController(ITravellersServices travellersServices)
        {
            _travellersServices = travellersServices;
        }

        ///<summary>
        ///Traer Hoteles y habitaciones disponibles
        ///</summary>
        [HttpGet]
        [Route(nameof(TravellersController.GetHotelByCondition))]
        public async Task<ResultResponse<List<HotelsByConditionDTO>>> GetHotelByCondition(DateTime entryDate, DateTime departureDate, int numberPeople, string city) => await _travellersServices.GetHotelByCondition(entryDate, departureDate, numberPeople, city);

        ///<summary>
        ///Crear una nueva reservacion
        ///</summary>
        [HttpPost]
        [Route(nameof(TravellersController.NewBooking))]
        public async Task<ResultResponse<string>> NewBooking(BookingDTO booking) => await _travellersServices.NewBooking(booking);

    }
}
