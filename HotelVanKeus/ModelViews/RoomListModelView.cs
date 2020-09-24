using HotelVanKeus.Models;
using System.Collections.Generic;

namespace HotelVanKeus.ModelViews
{
    public class RoomListModelView
    {
        public IEnumerable<Room> RoomsList { get; set; }
        //public string CurrentType { get; set; }
    }
}
