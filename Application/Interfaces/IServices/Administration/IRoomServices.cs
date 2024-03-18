using Domain.Models;
using Application.Utilities;
using Application.DTOs;

namespace Application.Interfaces.IServices.Administration
{
    public interface IRoomServices
    {
        Task<ResultResponse<Room>> NewRoom(RoomDTO room);
        Task<ResultResponse<string>> UpdateRoom(Guid idRoom, IDictionary<string, string> patchDoc);
        Task<ResultResponse<string>> DisabledOrEnabledRoom(Guid idRoom, bool Enabled);
    }
}
