
namespace Application.DTOs
{
    public class BookingDTO
    {
        public Guid RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public ContactBookingDTO ContactBooking { get; set; }
        public List<GuestsBookingDTO> Guests { get; set; }
    }
    public class GuestsBookingDTO
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

    public class ContactBookingDTO
    {
        public string FirstName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
