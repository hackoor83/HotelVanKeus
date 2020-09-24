using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelVanKeus.Models
{
    public class Reservation
    {
        public Reservation()
        {
            Checkin = DateTime.Now;
            Checkout = DateTime.Now;
        }

        [Key]
        public int ReservationId { get; set; }
       
        [Required]
        [DataType(DataType.Date)]
        public DateTime Checkin { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Checkout { get; set; }

        [Required]
        public Guest Guest { get; set; }

        public ReservationRoom ReservationRoom { get; set; }
    }
}
