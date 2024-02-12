using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QomfortHotelFinal.DAL;
using QomfortHotelFinal.Models;

namespace QomfortHotelFinal.Controllers
{
    public class ReservationController : Controller
    {
        private readonly AppDbContext _context;

        public ReservationController(AppDbContext context)
        {
           _context = context;
        }
        public IActionResult CreateReservation()
        {
            ViewData["CustomerId"] = new SelectList(_context.Set<AppUser>(), "Id", "Id");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateReservation([Bind("StartDate,EndDate,AppUserId")] Reservation booking)
        {
            if (ModelState.IsValid)
            {
                int roomId = -1;
                DateTime startDate = booking.ArrivalDateTime;
                DateTime endDate = booking.DeparturDateTime;

                if (startDate <= DateTime.Today || startDate > endDate)
                {
                    ViewData["AppUserId"] = new SelectList(_context.Set<AppUser>(), "Id", "Id", booking.AppUserId);
                    ViewBag.Status = "The start date cannot be in the past or later than the end date.";
                    return View(booking);
                }

                var activeBookings = _context.Reservations.Where(b => b.IsActive);
                foreach (var room in _context.Rooms)
                {
                    var activeBookingsForCurrentRoom = activeBookings.Where(b => b.RoomId == room.Id);
                    if (activeBookingsForCurrentRoom.All(b => startDate < b.ArrivalDateTime &&
                        endDate < b.ArrivalDateTime || startDate > b.DeparturDateTime && endDate > b.DeparturDateTime))
                    {
                        roomId = room.Id;
                        break;
                    }
                }

                if (roomId >= 0)
                {
                    booking.RoomId = roomId;
                    booking.IsActive = true;
                    _context.Reservations.Add(booking);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Home","Index");
                }
            }

            //ViewData["CustomerId"] = new SelectList(_context.Set<AppUser>(), "Id", "Id", booking.AppUserId);
            //ViewBag.Status = "The booking could not be created. There were no available room.";
            return View(booking);
        }
    }
}
