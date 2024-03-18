using Application.DTOs;
using Domain.Models;

namespace Application.Utilities
{
    public class RoomTotalCalculator
    {
        public decimal CalculateRoomTotal(RoomsByHotelDTO room, decimal hotelCommission, int days)
        {
            decimal totalHabitacion = (room.RoomsBaseCost + room.RoomsTaxes + (room.RoomsBaseCost * hotelCommission / 100))*days;
            return totalHabitacion;
        }
        public decimal CalculateRoomTotal(Room room, decimal hotelCommission, int days)
        {
            decimal totalRoom = (room.BaseCost + room.Taxes + (room.BaseCost * hotelCommission / 100)) * days;
            return totalRoom;
        }


    }
}
