using HotelVanKeus.Data;
using HotelVanKeus.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace HotelVanKeus.Controllers
{
    public class GuestController : Controller
    {
        private readonly HotelVanKeusContext _context;

        public GuestController(HotelVanKeusContext context)
        {
            _context = context;
        }
        public IActionResult List()
        {
            var model = _context.Guests.ToList();
            return View("List", model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        public IActionResult Create(Guest guest)
        {
            _context.Guests.Add(guest);
            _context.SaveChanges();
            return RedirectToAction("ConfirmationModal", guest);
        }

        public IActionResult Delete(int id)
        {
            var guest = _context.Guests.FirstOrDefault(e => e.Id == id);
            _context.Guests.Remove(guest);
            _context.SaveChanges();
            return RedirectToAction("List");
        }

        public IActionResult Edit(int id)
        {
            var guestToUpdate = _context.Guests.Find(id);
            return View("EditGuest", guestToUpdate);
        }

        [HttpPost]
        public IActionResult EditGuest(Guest guestToUpdate)
        {
            _context.Guests.Update(guestToUpdate);
            _context.SaveChanges();
            return RedirectToAction("ConfirmationModal", guestToUpdate);
        }

        public IActionResult ConfirmationModal(Guest guest)
        {
            return View("ConfirmationModal", guest);
        }

    }
}
