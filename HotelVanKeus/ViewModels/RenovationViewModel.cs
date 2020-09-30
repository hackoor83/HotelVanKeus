using HotelVanKeus.Models;
using System.Collections.Generic;

namespace HotelVanKeus.ViewModels
{
    public class RenovationViewModel
    {
        public Room Room { get; set; }
        
        public Reservation Reservation { get; set; }

        public List<Room> RoomsList { get; set; }

        public RenovationViewModel()
        {

        }
    }
}
