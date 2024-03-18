using Application.DTOs;
using Domain.Context;
using Domain.Enums;
using Domain.Models;
using Application.Interfaces.IRepository.Administration;
using Infraestructure.Repository.Common;
using Microsoft.EntityFrameworkCore;
using static Application.DTOs.HotelAndBooking;
using Microsoft.Identity.Client;

namespace Infraestructure.Repository.Administration
{
    public class HotelRepository : GenericRepository<Hotel>, IHotelRepository
    {
        public readonly ApplicationDbContext _context;
        public HotelRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<HotelBookingDTO>> GetAllBookings()
        {
            return await _context.Booking
                .Include(b => b.Hotel)
                .Include(b => b.Room)
                .Include(b => b.ContactEmergency)
                .Include(b => b.Guests)
                .GroupBy(b => b.Hotel)
                .Select(group => new HotelBookingDTO
                {
                    HotelId = group.Key.Id,
                    HotelName = group.Key.Name,
                    Address = group.Key.Address,
                    City = group.Key.City,
                    TotalCommission = group.Sum(b => b.TotalPrice / group.Key.Commission),
                    Bookings = group.Select(booking => new BookingInfoDTO
                    {
                        BookingId = booking.Id,
                        CheckInDate = booking.CheckInDate,
                        CheckOutDate = booking.CheckOutDate,
                        NumberOfPeople = booking.NumberOfPeople,
                        TotalPrice = booking.TotalPrice,
                        Status = booking.Status.ToString(),
                        Room = new RoomInfoDTO
                        {
                            RoomId = booking.Room.Id,
                            RoomLocation = booking.Room.Location,
                            RoomType = booking.Room.Type.ToString(),
                            BaseCost = booking.Room.BaseCost,
                            Taxes = booking.Room.Taxes,
                            Description = booking.Room.Description,
                            Guests = booking.Guests.Select(guest => new GuestDTO
                            {
                                FirstName = guest.FirstName,
                                LastName = guest.LastName,
                                BirthDate = guest.BirthDate,
                                Gender = guest.Gender,
                                DocumentType = guest.DocumentType,
                                DocumentNumber = guest.DocumentNumber,
                                Email = guest.Email,
                                PhoneNumber = guest.PhoneNumber
                            }).ToList()
                        },
                        ContactEmergency = new ContactEmergencyDTO
                        {
                            ContactEmergencyId = booking.ContactEmergency.Id,
                            FirstName = booking.ContactEmergency.FirstName,
                            PhoneNumber = booking.ContactEmergency.PhoneNumber
                        }
                    }).ToList()
                }).ToListAsync();

        }
        public async Task<List<HotelAndRoomDTO>> GetAllHotel()
        {
            return await _context.Hotel
                    .Include(x => x.Rooms)
                    .ToListAsync()
                    .ContinueWith(hotels =>
                    {
                        return hotels.Result.Select(x => new HotelAndRoomDTO()
                        {
                            HotelId = x.Id,
                            HotelName = x.Name,
                            HotelDescription = x.Description,
                            HotelAddress = x.Address,
                            HotelCommission = x.Commission,
                            HotelCity = x.City,
                            HotelStatus = Enum.GetName(typeof(HotelEnum), x.Status),
                            Rooms = x.Rooms.Select(room => new RoomsByHotelDTO
                            {
                                RoomsId = room.Id,
                                RoomsNumberPeople = room.NumberPeople,
                                RoomsLocation = room.Location,
                                RoomsType = Enum.GetName(typeof(RoomEnum), room.Type),
                                RoomsBaseCost = room.BaseCost,
                                RoomsTaxes = room.Taxes,
                                RoomsDescription = room.Description,
                                RoomsStatus = Enum.GetName(typeof(RoomStatus), room.Status)
                            }).ToList()
                        }).ToList();
                    });
        }
    }
}
