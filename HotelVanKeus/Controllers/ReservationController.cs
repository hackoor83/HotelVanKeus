using System;
using System.Linq;
using HotelVanKeus.Data;
using HotelVanKeus.Models;
using HotelVanKeus.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelVanKeus.Controllers
{
    public class ReservationController : Controller
    {
        //Global state variables==========================
        //private DateTime SelectedCheckin { get; set; }
        //private DateTime SelectedCheckout { get; set; }
        //================================================


        private readonly HotelVanKeusContext _context;

        public ReservationController(HotelVanKeusContext context)
        {
            _context = context;
        }
        public IActionResult List()
        {
            var reservationsList = _context.Reservations.ToList();
            return View("List", reservationsList);
        }

        //===============================================================
        //Reservation process
        //===============================================================

        //Step 1: Collect room requirements
        [HttpGet]
        public IActionResult CollectRoomRequirements() 
        {
            return View("CollectRoomRequirements");
        }

        //Step 2: Get list of available rooms that matches with room requirements
        [HttpPost]
        public IActionResult GetAvailableRooms(Room requiredRoom)
        {
            var roomsList = _context.Rooms.Include(r => r.ReservationRoom).ToList();

            var availableRooms = roomsList.Where(room => room.Size.Equals(requiredRoom.Size))
                .Where(room => room.Status.Equals(requiredRoom.Status))
                .Where(room => room.TypeRoom.Equals(requiredRoom.TypeRoom))
                .Where(room => room.ReservationRoom == null || (room.ReservationRoom.Reservation.Checkin > requiredRoom.ReservationRoom.Reservation.Checkout ||
                               room.ReservationRoom.Reservation.Checkout < requiredRoom.ReservationRoom.Reservation.Checkin))
                .ToList();

            if (availableRooms.Count() == 0)
            {
                //TO DO: return error message
            }

            var reservationViewModel = new ReservationViewModel(requiredRoom, availableRooms);
             
            return View("ListOfAvailableRooms", reservationViewModel);
        }


        //Step 3: Link selected room with guest
        [HttpGet]
        public IActionResult CollectGuestDetails(int selectedRoomId, DateTime checkin, DateTime checkout)
        {
            Room selectedRoom = _context.Rooms.Find(selectedRoomId);

            return View(selectedRoom);
        }

        [HttpPost]
        public IActionResult Create(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            _context.SaveChanges();
            return RedirectToAction(nameof(ConfirmationModal), reservation);
        }

        //================================================================

        public IActionResult Delete(int id)
        {
            var reservation = _context.Reservations.FirstOrDefault(e => e.ReservationId == id);
            _context.Reservations.Remove(reservation);
            _context.SaveChanges();
            return RedirectToAction("List");
        }

        public IActionResult Edit(int id)
        {
            var reservationToUpdate = _context.Reservations.Find(id);
            return View("EditReservation", reservationToUpdate);
        }

        [HttpPost]
        public IActionResult EditReservation(Reservation reservationToUpdate)
        {
            _context.Reservations.Update(reservationToUpdate);
            _context.SaveChanges();
            return RedirectToAction("ConfirmationModal", reservationToUpdate);
        }

        public IActionResult ConfirmationModal(Reservation reservation)
        {
            return View("ConfirmationModal", reservation);
        }

    }
}
