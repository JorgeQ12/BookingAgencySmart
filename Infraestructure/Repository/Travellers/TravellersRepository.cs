using Application.DTOs;
using Domain.Context;
using Domain.Models;
using Application.Interfaces.IRepository.Administration;
using Application.Interfaces.IRepository.Travellers;
using Infraestructure.Repository.Common;
using Microsoft.EntityFrameworkCore;
using Application.Utilities;

namespace Infraestructure.Repository.Administration
{
    public class TravellersRepository : GenericRepository<Booking>, ITravellersRepository
    {
        public readonly ApplicationDbContext _context;
        public readonly RoomTotalCalculator _roomTotalCalculator;
        public readonly IHotelRepository _hotelRepository;
        public TravellersRepository(ApplicationDbContext context, IHotelRepository hotelRepository, RoomTotalCalculator roomTotalCalculator ) : base(context)
        {
            _context = context;
            _hotelRepository = hotelRepository;
            _roomTotalCalculator = roomTotalCalculator;
        }

        public async Task<List<HotelsByConditionDTO>> GetHotelByCondition(string city, DateTime entryDate, DateTime departureDate, int numberPeople)
        {
            int days = departureDate.Subtract(entryDate).Days;
            IEnumerable<Booking> bookingActive = await GetAll();
            List<HotelAndRoomDTO> hotelAndRooms = await _hotelRepository.GetAllHotel();

            // Filtrar los hoteles que tienen al menos una reserva activa
            var availableHotels = hotelAndRooms
                .Where(hotel => hotel.HotelCity.Equals(city, StringComparison.OrdinalIgnoreCase))
                .SelectMany(hotel => hotel.Rooms
                    .Where(room => !bookingActive.Any(active =>
                        active.CheckInDate <= entryDate &&
                        active.CheckOutDate >= departureDate &&
                        active.RoomId.Equals(room.RoomsId)
                    ))
                    .Select(room => new { Hotel = hotel, Room = room }))
                .GroupBy(x => x.Hotel, x => x.Room)
                .Where(g => g.Any())
                .Select(g => new { Hotel = g.Key, AvailableRooms = g.ToList() })
                .ToList();

            // Calcular el total de cada habitación y construir la lista de DTO
            var hotelsByConditionDTOs = availableHotels.Select(hotel => new HotelsByConditionDTO
            {
                HotelId = hotel.Hotel.HotelId,
                HotelName = hotel.Hotel.HotelName,
                HotelAddress = hotel.Hotel.HotelAddress,
                Rooms = hotel.AvailableRooms.Select(room => new RoomsByConditionDTO
                {
                    RoomId = room.RoomsId,
                    RoomNumberPeople = room.RoomsNumberPeople,
                    RoomLocation = room.RoomsLocation,
                    RoomType = room.RoomsType,
                    TotalCost = _roomTotalCalculator.CalculateRoomTotal(room, hotel.Hotel.HotelCommission, days)
                }).ToList()
            }).ToList();

            return hotelsByConditionDTOs;
        }

        public async Task<List<Guest>> AddGuest(List<Guest> guests)
        {
            await _context.Set<Guest>().AddRangeAsync(guests);
            await _context.SaveChangesAsync();
            return guests;
        }

        public async Task<ContactEmergency> AddContact(ContactEmergency contactEmergency)
        {
            _context.Set<ContactEmergency>().Add(contactEmergency);
            await _context.SaveChangesAsync();
            return contactEmergency;
        }

        public async Task<Room> GetRoom(Guid idRoom)
        {
            return await _context.Room.Include(x => x.Hotel).Where(y => y.Id.Equals(idRoom)).FirstOrDefaultAsync();
        }

    }
}
