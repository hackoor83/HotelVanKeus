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

        [HttpGet]
        public IActionResult List()
        {
            var reservationsList = _context.Reservations
                .Include(r => r.Room)
                .Include(g => g.Guest)
                .ToList();

            //To make the room available after the checkout date:
            ResetRoomStatus();

            return View("List", reservationsList);
        }

        public IActionResult GuestReservations(int guestId)
        {
            Console.WriteLine(guestId);
            var guestReservationsList = _context.Reservations
                .Include(g => g.Guest)
                .Include(r => r.Room)
                .Where(r => r.Guest.Id == guestId).ToList();

            return View("List", guestReservationsList);
        }


        //===============================================================
        //Reservation process
        //===============================================================

        //Step 1: Collect room requirements
        [HttpGet]
        public IActionResult CollectRoomRequirements() 
        {
            //To make the room available after the checkout date:
            ResetRoomStatus();

            return View("CollectRoomRequirements");
        }


        //Step 2: Get list of available rooms that matches with room requirements
        [HttpPost]
        public IActionResult GetAvailableRooms(Reservation tempReservation)
        {

            var roomsList = _context.Rooms.Include(r => r.Reservations).ToList();

            var availableRooms = roomsList.Where(room => room.Size.Equals(tempReservation.Room.Size))
                                          .Where(room => room.Status.Equals(tempReservation.Room.Status))
                                          .Where(room => room.TypeRoom.Equals(tempReservation.Room.TypeRoom))
                                          .Where(room => room.Reservations.Count == 0 || room.Reservations.Any(item => item.Checkin > tempReservation.Checkout) &&
                                                                                         room.Reservations.Any(item => item.Checkout < tempReservation.Checkin))                              
                                          .ToList();

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
        public IActionResult CollectGuestDetails(int selectedRoomId, string checkin, string checkout)
        {

            Room selectedRoom = _context.Rooms.Find(selectedRoomId);

            var listOfGuests = _context.Guests.ToList();

            var lastViewModel = new ReservationViewModel
            {
                GuestsList = listOfGuests,
                RequiredRoom = selectedRoom,
                TempCheckin = DateTime.Parse(checkin),
                TempCheckout = DateTime.Parse(checkout)
            };
           
            return View("CollectGuestDetails", lastViewModel);
        }


        //Step 4: Combine all info and confirm reservation
        [HttpGet]
        public IActionResult ConfirmReservation(int selectedGuestId, int selectedRoomId, string checkin, string checkout)
        {
            var guestReservation = _context.Guests.Find(selectedGuestId);
            var roomReservation = _context.Rooms.Find(selectedRoomId);

            var confirmedReservation = new Reservation(guestReservation, roomReservation, DateTime.Parse(checkin), DateTime.Parse(checkout));

            _context.Reservations.Add(confirmedReservation);
            _context.SaveChanges();
            return View("ConfirmationModal", confirmedReservation);
        }

        //================================================================

        [HttpGet]
        //public IActionResult Delete(int id, int roomId)
        public IActionResult Delete(int id)
        {
            var reservationSelected = _context.Reservations.Include(r => r.Room).FirstOrDefault(res => res.Id == id);

            reservationSelected.Room.Status = StatusEnum.Available;
           
            _context.Reservations.Remove(reservationSelected);
            _context.SaveChanges();
            return RedirectToAction(nameof(List));
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

        public void ResetRoomStatus()
        {
            var reservationsList = _context.Reservations
                .Include(r => r.Room)
                .Include(g => g.Guest)
                .ToList();

            foreach (var reservation in reservationsList)
            {
                if (DateTime.Now > reservation.Checkout & reservation.Room.Status == StatusEnum.Reserved)
                {
                    reservation.Room.Status = StatusEnum.Available;
                }
            }
            _context.SaveChanges();
        }

        //============================================================
        //      Renovations
        //============================================================

        [HttpGet]
        public IActionResult ListOfAvailableRooms()
        {
            var listOfRooms = _context.Rooms
                .Where(room => room.Status == StatusEnum.Available)
                .ToList();

            return View("ListRoomsToRenovate", listOfRooms);
        }

        [HttpGet]
        public IActionResult ChangeRenovationStatus(int id)
        {
            var selectedRoom = _context.Rooms.Find(id);

            if(selectedRoom.Status == StatusEnum.Available)
            {
                selectedRoom.Status = StatusEnum.Renovation;

            }else if(selectedRoom.Status == StatusEnum.Renovation)
            {
                selectedRoom.Status = StatusEnum.Available;
            }

            _context.SaveChanges();

            return View("RenovationConfirmationModal", selectedRoom);
        }

        [HttpGet]
        public IActionResult ListOfOngoingRenovations()
        {
            var renovationsList = _context.Rooms
                .Where(room => room.Status.Equals(StatusEnum.Renovation))
                .ToList();

            return View("ListOfOngoingRenovations", renovationsList);
        }

    }
}
