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
using static Application.DTOs.HotelAndBooking;

namespace Application.Services.Administration
{
    public class HotelServices : IHotelServices
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;
        public HotelServices(IHotelRepository hotelRepository, IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        public async Task<ResultResponse<List<HotelBookingDTO>>> GetAllBookings()
        {
            try
            {
                List<HotelBookingDTO> hotels = await _hotelRepository.GetAllBookings();
                return new ResultResponse<List<HotelBookingDTO>>(true, hotels);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<ResultResponse<List<HotelAndRoomDTO>>> GetAllHotel()
        {
            try
            {
                List<HotelAndRoomDTO> hotels = await _hotelRepository.GetAllHotel();
                return new ResultResponse<List<HotelAndRoomDTO>> (true, hotels);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<ResultResponse<Hotel>> NewHotel(HotelDTO hotel)
        {
            try
            {
                //Validar El DTO que este lleno
                HotelDTOValidator validations = new HotelDTOValidator();
                ValidationResult result = validations.Validate(hotel);
                if (!result.IsValid)
                {
                    StringBuilder errorMessage = new StringBuilder();
                    result.Errors.ForEach(x => errorMessage.AppendLine($"{x.ErrorMessage}"));

                    return new ResultResponse<Hotel>(false, errorMessage.ToString());
                }

                Hotel hotelAdd = _mapper.Map<HotelDTO, Hotel>(hotel);
                await _hotelRepository.Add(hotelAdd);

                return new ResultResponse<Hotel>(true, hotelAdd);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ResultResponse<string>> UpdateHotel(Guid idHotel, IDictionary<string, string> patchDoc)
        {
            try
            {
                if (idHotel.Equals(Guid.Empty))
                {
                    return new ResultResponse<string>(false, GlobalResponses.GuidEmpty);
                }
                else
                {
                    var existHotel = await _hotelRepository.GetById(idHotel);
                    if (existHotel == null)
                    {
                        return new ResultResponse<string>(false, GlobalResponses.NotFoundHotel);
                    }
                    else
                    {
                        foreach (var item in patchDoc)
                        {
                            var property = existHotel.GetType().GetProperty(item.Key);
                            if (property != null)
                            {
                                property.SetValue(existHotel, item.Value);
                            }
                            else
                            {
                                return new ResultResponse<string>(false, GlobalResponses.NameParamInvalid);
                            }
                        }

                        await _hotelRepository.Update(existHotel);
                        return new ResultResponse<string>(true, GlobalResponses.UpdateSucces);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ResultResponse<string>> DisabledOrEnabledHotel(Guid idHotel, bool Enabled)
        {
            try
            {
                if (idHotel.Equals(Guid.Empty))
                {
                    return new ResultResponse<string>(false, GlobalResponses.GuidEmpty);
                }
                else
                {
                    var existHotel = await _hotelRepository.GetById(idHotel);
                    if (existHotel == null)
                    {
                        return new ResultResponse<string>(false, GlobalResponses.NotFoundHotel);
                    }
                    else
                    {
                        existHotel.Status = Enabled ? HotelEnum.Enabled : HotelEnum.Disabled;
                        await _hotelRepository.Update(existHotel);

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
