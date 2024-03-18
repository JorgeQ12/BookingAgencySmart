using Application.Interfaces.IRepository.Administration;
using Application.Interfaces.IServices.Administration;
using Application.Utilities;
using Domain.Models;
using Application.DTOs;
using AutoMapper;
using Domain.Enums;
using Application.Validators;
using System.Text;
using FluentValidation.Results;
using Application.Interfaces.IRepository.Common;

namespace Application.Services.Administration
{
    public class RoomServices : IRoomServices
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;
        public RoomServices(IRoomRepository roomRepository, IMapper mapper)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
        }

        public async Task<ResultResponse<Room>> NewRoom(RoomDTO room)
        {
            try
            {
                //Validar El DTO que este lleno
                RoomDTOValidator validations = new RoomDTOValidator();
                ValidationResult result = validations.Validate(room);
                if (!result.IsValid)
                {
                    StringBuilder errorMessage = new StringBuilder();
                    result.Errors.ForEach(x => errorMessage.AppendLine($"{x.ErrorMessage}"));

                    return new ResultResponse<Room>(false, errorMessage.ToString());
                }

                //Guardamos despues de validar
                Room roomAdd = _mapper.Map<RoomDTO, Room>(room);
                await _roomRepository.Add(roomAdd);

                return new ResultResponse<Room>(true, roomAdd);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ResultResponse<string>> UpdateRoom(Guid idRoom, IDictionary<string, string> patchDoc)
        {
            try
            {
                if (idRoom.Equals(Guid.Empty))
                {
                    return new ResultResponse<string>(false, GlobalResponses.GuidEmpty);
                }
                else
                {
                    var exisRoom = await _roomRepository.GetById(idRoom);
                    if(exisRoom == null)
                    {
                        return new ResultResponse<string>(false, GlobalResponses.NotFoundRoom);
                    }
                    else
                    {
                        foreach (var item in patchDoc)
                        {
                            var property = exisRoom.GetType().GetProperty(item.Key);
                            if (property != null)
                            {
                                property.SetValue(exisRoom, item.Value);
                            }
                            else
                            {
                                return new ResultResponse<string>(false, GlobalResponses.NameParamInvalid);
                            }
                        }

                        await _roomRepository.Update(exisRoom);
                        return new ResultResponse<string>(true, GlobalResponses.UpdateSucces);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ResultResponse<string>> DisabledOrEnabledRoom(Guid idRoom, bool Enabled)
        {
            try
            {
                if (idRoom.Equals(Guid.Empty))
                {
                    return new ResultResponse<string>(false, GlobalResponses.GuidEmpty);
                }
                else
                {
                    var exisRoom = await _roomRepository.GetById(idRoom);
                    if (exisRoom == null)
                    {
                        return new ResultResponse<string>(false, GlobalResponses.NotFoundRoom);
                    }
                    else
                    {
                        exisRoom.Status = Enabled ? RoomStatus.Enabled : RoomStatus.Disabled;
                        await _roomRepository.Update(exisRoom);

                        return new ResultResponse<string>(true, GlobalResponses.UpdateSucces);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
