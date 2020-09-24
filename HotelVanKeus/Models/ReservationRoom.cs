using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelVanKeus.Models
{
    public class ReservationRoom
    {
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }
    }
}
