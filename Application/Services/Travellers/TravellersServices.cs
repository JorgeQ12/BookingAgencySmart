using Application.Utilities;
using Application.DTOs;
using AutoMapper;
using Application.Interfaces.IServices.Travellers;
using Application.Interfaces.IRepository.Travellers;
using Domain.Models;
using Domain.Enums;
using Application.Validators;
using FluentValidation.Results;
using System.Text;

namespace Application.Services.Travellers
{
    public class TravellersServices : ITravellersServices
    {
        public readonly RoomTotalCalculator _roomTotalCalculator;
        private readonly SendEmail _sendEmail;
        private readonly ITravellersRepository _travellersRepository;
        private readonly IMapper _mapper;
        public TravellersServices(ITravellersRepository travellersRepository, IMapper mapper, RoomTotalCalculator roomTotalCalculator , SendEmail sendEmail)
        {
            _travellersRepository = travellersRepository;
            _mapper = mapper;
            _roomTotalCalculator = roomTotalCalculator;
            _sendEmail = sendEmail;
        }

        public async Task<ResultResponse<List<HotelsByConditionDTO>>> GetHotelByCondition(DateTime entryDate, DateTime departureDate, int numberPeople, string city)
        {
            try
            {
                if (!ValidateDates(entryDate, departureDate))
                {
                    return new ResultResponse<List<HotelsByConditionDTO>>(false, GlobalResponses.InvalidDates);
                }

                if (string.IsNullOrEmpty(city) || numberPeople <= 0)
                {
                    return new ResultResponse<List<HotelsByConditionDTO>>(false, GlobalResponses.InvalidCityOrName);
                }

                List<HotelsByConditionDTO> hotels = await _travellersRepository.GetHotelByCondition(city, entryDate, departureDate, numberPeople);
                return new ResultResponse<List<HotelsByConditionDTO>> (true, hotels);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ResultResponse<string>> NewBooking(BookingDTO booking)
        {
            try
            {
                BookingDTOValidator validations = new BookingDTOValidator();
                ValidationResult result = validations.Validate(booking);
                if (!result.IsValid)
                {
                    StringBuilder errorMessage = new StringBuilder();
                    result.Errors.ForEach(x => errorMessage.AppendLine($"{x.ErrorMessage}"));

                    return new ResultResponse<string>(false, errorMessage.ToString());
                }

                //Continuidad del codigo
                int days = booking.CheckOutDate.Subtract(booking.CheckInDate).Days;
                Room roomExist = await _travellersRepository.GetRoom(booking.RoomId);
                ContactEmergency contact = _mapper.Map<ContactBookingDTO, ContactEmergency>(booking.ContactBooking);

                Booking newBooking = new Booking()
                {
                    Id = Guid.NewGuid(),
                    HotelId = roomExist.HotelId,
                    RoomId = roomExist.Id,
                    ContactEmergencyId = contact.Id,
                    CheckInDate = booking.CheckInDate,
                    CheckOutDate = booking.CheckOutDate,
                    NumberOfPeople = roomExist.NumberPeople,
                    TotalPrice = _roomTotalCalculator.CalculateRoomTotal(roomExist, roomExist.Hotel.Commission, days),
                    Status = ReservationEnum.Confirmed
                };

                List<Guest> guests = _mapper.Map<List<GuestsBookingDTO>, List<Guest>>(booking.Guests);
                guests.ForEach(guest => {guest.ReservationId = newBooking.Id;guest.Id = Guid.NewGuid();});

                // Agregar el contacto de emergencia, la reserva y los huéspedes
                await _travellersRepository.AddContact(contact);
                await _travellersRepository.Add(newBooking);
                await _travellersRepository.AddGuest(guests);

                //Enviar correo de confirmacion
                _sendEmail.Main(guests);
                return new ResultResponse<string>(true, GlobalResponses.ReservaInsertada);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private bool ValidateDates(DateTime entryDate, DateTime departureDate)
        {
            if (entryDate == DateTime.MinValue || departureDate == DateTime.MinValue)
            {
                return false;
            }

            if (departureDate < entryDate)
            {
                return false;
            }

            return true;
        }


    }
}
