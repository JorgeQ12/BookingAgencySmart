using Application.Utilities;
using Application.Interfaces.IServices.Administration;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace BookingAgency.Controllers.Administration
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RoomController : Controller
    {
        private readonly IRoomServices _roomServices;

        public RoomController(IRoomServices roomServices)
        {
            _roomServices = roomServices;
        }

        ///<summary>
        ///Crear una Habitacion asociada a un hotel
        ///</summary>
        [HttpPost]
        [Route(nameof(RoomController.NewRoom))]
        public async Task<ResultResponse<Room>> NewRoom(RoomDTO room) => await _roomServices.NewRoom(room);

        ///<summary>
        ///Actualizar habitacion
        ///</summary>
        [HttpPatch]
        [Route(nameof(RoomController.UpdateRoom))]
        public async Task<ResultResponse<string>> UpdateRoom(Guid idRoom, IDictionary<string, string> patchDoc) => await _roomServices.UpdateRoom(idRoom, patchDoc);

        ///<summary>
        ///Deshabilitar Habitacion
        ///</summary>
        [HttpPut]
        [Route(nameof(RoomController.DisabledOrEnabledRoom))]
        public async Task<ResultResponse<string>> DisabledOrEnabledRoom(Guid idRoom, bool Enabled) => await _roomServices.DisabledOrEnabledRoom(idRoom, Enabled);

    }
}
