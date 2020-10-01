using HotelVanKeus.Data;
using HotelVanKeus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelVanKeus.ViewModels
{
    public class GuestViewModel
    {
        public readonly HotelVanKeusContext _context;

        public Guest Guest{ get; set; }
        //public List<Guest> GuestsList { get; set; }
    }
}
