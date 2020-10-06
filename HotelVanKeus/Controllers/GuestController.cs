using HotelVanKeus.Data;
using HotelVanKeus.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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
            var model = _context.Guests.Include(r => r.Reservations).ToList();
            return View("List", model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        public IActionResult Create(Guest newGuest)
        {
            //Check if the guest is already in the system:
            if(_context.Guests.Any(g => g.FirstName.Equals(newGuest.FirstName) & g.LastName.Equals(newGuest.LastName) | g.Email.Equals(newGuest.Email)))
            {
                ViewBag.Title = "Guest already in the database!";
                ViewBag.Message = $"A guest with first name: {newGuest.FirstName}, last name: {newGuest.LastName}, " +
                    $"and email address: {newGuest.Email}, already exist in the database. " +
                    "Please review the new guest details, or search for this guest in the Guests List.";
                return View("_errorGeneral");
            }

            //Check if the email address is valid:
            Regex regex = new Regex(@"^[\w0-9._%+-]+@[\w0-9.-]+\.[\w]{2,6}$");
            if (!regex.IsMatch(newGuest.Email))
            {
                ViewBag.Title = "Email address is not valid!";
                ViewBag.Message = $"The email address is not valid. Please review.";
                return View("_errorGeneral");
            }

            _context.Guests.Add(newGuest);
            _context.SaveChanges();
            return RedirectToAction("ConfirmationModal", newGuest);
        }

        public IActionResult Delete(int id)
        {
            var guest = _context.Guests.FirstOrDefault(e => e.Id == id);

            try
            {
                _context.Guests.Remove(guest);
                _context.SaveChanges();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                ViewBag.Message = "There is at least one reservation linked to this guest. Please try again after the checkout or delete any linked reservations before deleting the guest.";
                ViewBag.Title = "Cannot delete this guest right now!";
                return View("_errorGeneral");
            }
            
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

        //Search for guest
        [HttpGet]
        public IActionResult Search(string searchInput)
        {

            var listOfMatches = _context.Guests.Include(r => r.Reservations)
                .Where(guest => guest.Id.Equals(searchInput) | guest.FirstName.Contains(searchInput) | guest.LastName.Contains(searchInput) |
                guest.Telephone.Equals(searchInput) | guest.Postcode.Contains(searchInput) | guest.Email.Contains(searchInput) | 
                guest.Address.Contains(searchInput) | guest.City.Contains(searchInput) | guest.Country.Contains(searchInput))
                .ToList();

            return View("List", listOfMatches);
        }

    }
}
