using HotelVanKeus.Data;
using HotelVanKeus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelVanKeus.ViewModels
{
    public class ReservationViewModel
    {
        public readonly HotelVanKeusContext _context;

        public Room RequiredRoom { get; set; }
        public List<Room> AvailableRooms { get; set; }
        public Guest Guest { get; set; }

        public ReservationViewModel(Room room, List<Room> availableRooms)
        {
            RequiredRoom = room;
            AvailableRooms = availableRooms;
        }

    }
}
