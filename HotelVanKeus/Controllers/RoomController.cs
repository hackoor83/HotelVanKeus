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
            _context.Rooms.Add(room);
            _context.SaveChanges();
            return RedirectToAction("ConfirmationModal", room);
        }

        public IActionResult Delete(int id)
        {
            var room = _context.Rooms.FirstOrDefault(e => e.Id == id);
            _context.Rooms.Remove(room);
            _context.SaveChanges();
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
