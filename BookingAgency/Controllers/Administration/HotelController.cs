using Application.Utilities;
using Application.Interfaces.IServices.Administration;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using static Application.DTOs.HotelAndBooking;
using Microsoft.AspNetCore.Authorization;

namespace BookingAgency.Controllers.Administration
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HotelController : Controller
    {
        private readonly IHotelServices _hotelServices;

        public HotelController(IHotelServices hotelServices)
        {
            _hotelServices = hotelServices;
        }

        ///<summary>
        ///Trear todas las reservaciones
        ///</summary>
        [HttpGet]
        [Route(nameof(HotelController.GetAllBookings))]
        public async Task<ResultResponse<List<HotelBookingDTO>>> GetAllBookings() => await _hotelServices.GetAllBookings();

        ///<summary>
        ///Trear todos los Hoteles y habitaciones asociadas
        ///</summary>
        [HttpGet]
        [Route(nameof(HotelController.GetAllHotel))]
        public async Task<ResultResponse<List<HotelAndRoomDTO>>> GetAllHotel() => await _hotelServices.GetAllHotel();

        ///<summary>
        ///Crear un nuevo Hotel
        ///</summary>
        [HttpPost]
        [Route(nameof(HotelController.NewHotel))]
        public async Task<ResultResponse<Hotel>> NewHotel(HotelDTO hotels) => await _hotelServices.NewHotel(hotels);

        ///<summary>
        ///Actualizar el Hotel
        ///</summary>
        [HttpPatch]
        [Route(nameof(HotelController.UpdateHotel))]
        public async Task<ResultResponse<string>> UpdateHotel(Guid idHotel, IDictionary<string, string> patchDoc) => await _hotelServices.UpdateHotel(idHotel, patchDoc);

        ///<summary>
        ///Deshabilitar Hotel
        ///</summary>
        [HttpPut]
        [Route(nameof(HotelController.DisabledOrEnabledHotel))]
        public async Task<ResultResponse<string>> DisabledOrEnabledHotel(Guid idHotel, bool Enabled) => await _hotelServices.DisabledOrEnabledHotel(idHotel, Enabled);

    }
}
