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
        public IActionResult GetAvailableRooms(Reservation tempReservation)
        {
            //var roomsList = _context.Rooms.Include(r => r.ReservationRoom).ToList();
            var roomsList = _context.Rooms.ToList();

            var availableRooms = roomsList.Where(room => room.Size.Equals(tempReservation.Room.Size))
                .Where(room => room.Status.Equals(tempReservation.Room.Status))
                .Where(room => room.TypeRoom.Equals(tempReservation.Room.TypeRoom))
                .Where(room => room.Reservations == null || room.Reservations.Any(item => item.Checkin > tempReservation.Checkout)  &&
                               room.Reservations.Any(item => item.Checkout < tempReservation.Checkin))
                .ToList();

            //.Where(room => room.ReservationRoom == null || (room.ReservationRoom.Reservation.Checkin > reservationWithRequiredRoom.ReservationRoom.Reservation.Checkout ||


            if (availableRooms.Count() == 0)
            {
                //TO DO: return error message
            }

            //var reservationViewModel = new ReservationViewModel(reservationWithRequiredRoom, availableRooms);
            var newReservationViewModel = new ReservationViewModel
            {
                TempCheckin = tempReservation.Checkin,
                TempCheckout = tempReservation.Checkout,
                AvailableRooms = availableRooms,
                NewReservation = tempReservation
            };
             
            return View("ListOfAvailableRooms", newReservationViewModel);
        }


        //Step 3: Link selected room with guest
        [HttpGet]
        public IActionResult CollectGuestDetails(int selectedRoomId, DateTime checkin, DateTime checkout)
        {
            Room selectedRoom = _context.Rooms.Find(selectedRoomId);

            var listOfGuests = _context.Guests.ToList();

            var lastViewModel = new ReservationViewModel
            {
                GuestsList = listOfGuests,
                RequiredRoom = selectedRoom,
                TempCheckin = checkin,
                TempCheckout = checkout
            };

            return View("CollectGuestDetails", lastViewModel);
        }

        //Step 4: Combine all info and confirm reservation
        [HttpGet]
        public IActionResult ConfirmReservation(int selectedGuestId, int selectedRoomId, DateTime checkin, DateTime checkout)
        {
            var guestReservation = _context.Guests.Find(selectedGuestId);
            var roomReservation = _context.Rooms.Find(selectedRoomId);

            var confirmedReservation = new Reservation(guestReservation, roomReservation, checkin, checkout);

            _context.Reservations.Add(confirmedReservation);
            _context.SaveChanges();
            return RedirectToAction("ConfirmationModal", confirmedReservation);
        }

        //================================================================

        public IActionResult Delete(int id)
        {
            var reservation = _context.Reservations.FirstOrDefault(e => e.Id == id);
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
