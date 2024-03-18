using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class HotelAndBooking
    {
        public class HotelBookingDTO
        {
            public Guid HotelId { get; set; }
            public string HotelName { get; set; }
            public string City { get; set; }
            public string Address { get; set; }
            public decimal TotalCommission { get; set; }
            public List<BookingInfoDTO> Bookings { get; set; }
        }

        public class BookingInfoDTO
        {
            public Guid BookingId { get; set; }
            public DateTime CheckInDate { get; set; }
            public DateTime CheckOutDate { get; set; }
            public int NumberOfPeople { get; set; }
            public decimal TotalPrice { get; set; }
            public string Status { get; set; }
            public RoomInfoDTO Room { get; set; }
            public ContactEmergencyDTO ContactEmergency { get; set; }
        }

        public class RoomInfoDTO
        {
            public Guid RoomId { get; set; }
            public string RoomLocation { get; set; }
            public string RoomType { get; set; }
            public decimal BaseCost { get; set; }
            public decimal Taxes { get; set; }
            public string Description { get; set; }
            public List<GuestDTO> Guests { get; set; }
        }
        public class GuestDTO
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public DateTime BirthDate { get; set; }
            public string Gender { get; set; }
            public string DocumentType { get; set; }
            public string DocumentNumber { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
        }
        public class ContactEmergencyDTO
        {
            public Guid ContactEmergencyId { get; set; }
            public string FirstName { get; set; }
            public string PhoneNumber { get; set; }
        }
    }
}
