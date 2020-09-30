using System;
using System.ComponentModel.DataAnnotations;

namespace HotelVanKeus.Models
{
    public class Reservation
    {
       
        [Key]
        public int Id { get; set; }
       
        [Required]
        [DataType(DataType.Date)]
        public DateTime Checkin { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Checkout { get; set; }

        public Guest Guest { get; set; }

        public Room Room { get; set; }

        public Reservation() {}

        public Reservation(Guest guest, Room room, DateTime checkin, DateTime checkout)
        {
            Guest = guest;
            Room = room;
            Checkin = checkin;
            Checkout = checkout;

            //change the room status to Reserved when a reservation is created:
            Room.Status = StatusEnum.Reserved;
        }
    }
}
