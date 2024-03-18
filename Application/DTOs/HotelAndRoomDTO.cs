namespace Application.DTOs
{
    public class HotelAndRoomDTO
    {
        public Guid HotelId { get; set; }
        public string HotelName { get; set; }
        public string HotelDescription { get; set; }
        public string HotelCity { get; set; }
        public string HotelAddress { get; set; }
        public decimal HotelCommission { get; set; }
        public string HotelStatus { get; set; }
        public List<RoomsByHotelDTO> Rooms { get; set; }
    }

    public class RoomsByHotelDTO
    {
        public Guid RoomsId { get; set; }
        public int RoomsNumberPeople { get; set; }
        public string RoomsLocation { get; set; }
        public string RoomsType { get; set; }
        public decimal RoomsBaseCost { get; set; }
        public decimal RoomsTaxes { get; set; }
        public string RoomsDescription { get; set; }
        public string RoomsStatus { get; set; }
    }

    public class HotelsByConditionDTO
    {
        public Guid HotelId { get; set; }
        public string HotelName { get; set; }
        public string HotelAddress { get; set; }
        public List<RoomsByConditionDTO> Rooms { get; set; }
    }

    public class RoomsByConditionDTO
    {
        public Guid RoomId { get; set; }
        public string RoomLocation { get; set; }
        public string RoomType { get; set; }
        public int RoomNumberPeople{ get; set; }
        public decimal TotalCost { get; set; }
    }
}
