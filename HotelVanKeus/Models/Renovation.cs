using System;
using System.ComponentModel.DataAnnotations;

namespace HotelVanKeus.Models
{
    public class Renovation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ContactPerson { get; set; }

        [Required]
        public DateTime FromDate { get; set; }

        [Required]
        public DateTime ToDate { get; set; }

        [Required]
        public Room Room { get; set; }

    }
}
