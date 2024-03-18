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
    public class Room
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("Hotel")]
        public Guid HotelId { get; set; }
        public int NumberPeople { get; set; }
        public string Location { get; set; }
        public RoomEnum Type { get; set; }
        public decimal BaseCost { get; set; }
        public decimal Taxes { get; set; }
        public string Description { get; set; }
        public RoomStatus Status { get; set; }

        public virtual Hotel Hotel { get; set; }
    }
}
