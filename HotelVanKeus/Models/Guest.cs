using System.Collections.Generic;

namespace HotelVanKeus.Models
{
    public class Guest
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Telephone { get; set; }
        public string Email { get; set; }
        public List<Reservation> Reservations { get; set; }
    }
}
