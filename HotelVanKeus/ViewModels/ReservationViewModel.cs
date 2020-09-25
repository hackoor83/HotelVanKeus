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
        public Reservation NewReservation { get; set; }
        public List<Room> AvailableRooms { get; set; }
        public List<Guest> GuestsList { get; set; }

        public DateTime TempCheckin { get; set; }
        public DateTime TempCheckout{ get; set; }

        public ReservationViewModel(Reservation reservation, List<Room> availableRooms)
        {
            NewReservation = reservation;
            AvailableRooms = availableRooms;
        }

        public ReservationViewModel()
        {
                
        }

    }
}
