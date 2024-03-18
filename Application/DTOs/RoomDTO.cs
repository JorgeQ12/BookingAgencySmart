using Domain.Enums;

namespace Application.DTOs
{
    public class RoomDTO
    {
        public Guid HotelId { get; set; }
        public int NumberPeople { get; set; }
        public string Location { get; set; }
        public RoomEnum Type { get; set; }
        public decimal BaseCost { get; set; }
        public decimal Taxes { get; set; }
        public string Description { get; set; }
    }
}
