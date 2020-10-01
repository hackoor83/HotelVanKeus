using HotelVanKeus.Data;
using HotelVanKeus.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace HotelVanKeus.Controllers
{
    public class RoomController : Controller
    {

        private readonly HotelVanKeusContext _context;

        public RoomController(HotelVanKeusContext context)
        {
            _context = context;
        }

        public IActionResult List()
        {
            var model = _context.Rooms.ToList();
            return View("List", model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        public IActionResult Create(Room room)
        {
            //Check if the room is already in the system:
            if (_context.Rooms.Any(r => r.RoomNumber.Equals(room.RoomNumber)))
            {
                ViewBag.Title = "Room already in the database!";
                ViewBag.Message = $"Room number {room.RoomNumber} already exists in the database. " +
                    "Please review the new room details, or search for this room in the Rooms List.";
                return View("_errorGeneral");
            }

            _context.Rooms.Add(room);
            _context.SaveChanges();
            return RedirectToAction("ConfirmationModal", room);
        }

        public IActionResult Delete(int id)
        {
            var room = _context.Rooms.FirstOrDefault(e => e.Id == id);

            try {
                _context.Rooms.Remove(room);
                _context.SaveChanges();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                ViewBag.Title = "Cannot delete this room right now!";
                ViewBag.Message = "There is at least one reservation linked to this room. Please try again after the checkout or delete any linked reservations before deleting the room.";
                return View("_errorGeneral");
            }
            
            return RedirectToAction("List");
        }

        public IActionResult Edit(int id)
        { 
            var roomToUpdate = _context.Rooms.Find(id);
            return View("EditRoom", roomToUpdate);
        }
        
        [HttpPost]
        public IActionResult EditRoom(Room roomToUpdate)
        {
            _context.Rooms.Update(roomToUpdate);
            _context.SaveChanges();
            return RedirectToAction("ConfirmationModal", roomToUpdate);
        }

        public IActionResult ConfirmationModal(Room room)
        {
            return View("ConfirmationModal", room);
        }

    }
}
