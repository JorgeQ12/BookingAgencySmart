using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Booking
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("Hotel")]
        public Guid HotelId { get; set; }
        [ForeignKey("Room")]
        public Guid RoomId { get; set; }
        [ForeignKey("ContactEmergency")]
        public Guid ContactEmergencyId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfPeople { get; set; }
        public decimal TotalPrice { get; set; }
        public ReservationEnum Status { get; set; }

        public virtual Hotel Hotel { get; set; }
        public virtual Room Room { get; set; }
        public virtual ContactEmergency ContactEmergency { get; set; }
        public virtual ICollection<Guest> Guests { get; set; }
    }

}
