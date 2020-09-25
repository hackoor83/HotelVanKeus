using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        //public int GuestId { get; set; }
        public Guest Guest { get; set; } = new Guest();

        //[ForeignKey("Id")]
        //public int Id { get; set; }
        public Room Room { get; set; } = new Room();

        //public ReservationRoom ReservationRoom { get; set; }

        public Reservation()
        {
            Checkin = DateTime.Now;
            Checkout = DateTime.Now;
        }

        public Reservation(Guest guest, Room room, DateTime checkin, DateTime checkout)
        {
            //GuestId = guest.Id;
            Guest = guest;
            //Id = room.Id;
            Room = room;
            Checkin = checkin;
            Checkout = checkout;
        }
    }
}
