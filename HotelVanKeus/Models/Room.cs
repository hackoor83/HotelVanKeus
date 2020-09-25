using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelVanKeus.Models
{
    public class Room
    {
        public int Id { get; set; }

        [Required]
        [Range(001, 999, ErrorMessage ="Use number between 001 and 999.")]
        public int RoomNumber { get; set; }

        [Required]
        [MaxLength(10)]
        public string Size { get; set; }
        [Required]
        public TypeEnum TypeRoom { get; set; }
        [Required]
        public StatusEnum Status { get; set; }

        public List<Reservation> Reservations { get; set; }

    }
}
