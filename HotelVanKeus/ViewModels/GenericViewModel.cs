using HotelVanKeus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelVanKeus.ViewModels
{
    public class GenericViewModel
    {
        public Room Room { get; set; }
        public Reservation Reservation { get; set; }
        public Guest Guest { get; set; }
        public List<Guest> GuestsList { get; set; }
        public List<Room> RoomsList { get; set; }
        public List<Reservation> ReservationsList { get; set; }

        public GenericViewModel()
        {

        }
        
    }
}
