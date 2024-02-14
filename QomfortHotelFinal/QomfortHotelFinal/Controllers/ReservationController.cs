using MailKit.Search;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using QomfortHotelFinal.DAL;
using QomfortHotelFinal.Models;
using QomfortHotelFinal.ViewModels;
using System.Security.Claims;

namespace QomfortHotelFinal.Controllers
{
    public class ReservationController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public ReservationController(AppDbContext context,UserManager<AppUser> userManager)
        {
           _context = context;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task< IActionResult> CreateReservation()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> CreateReservation(RoomVM vm)
        {
           
                AppUser user = await _userManager.Users
           .Include(u => u.Reservations)
           .ThenInclude(bi => bi.Room)
           .FirstOrDefaultAsync(u => u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));


                if (!ModelState.IsValid) return View(vm);


                // Find the room
                var room = _context.Rooms.Find(vm.RoomId);
                if (room == null)
                {
                    return NotFound();
                }




                // Calculate total price based on selected dates
                decimal totalPrice = (vm.DeparturDate - vm.ArrivalDate).Days * room.Price;

                // Create reservation
                var reservation = new Reservation
                {
                    ArrivalDate = vm.ArrivalDate,
                    DeparturDate = vm.DeparturDate,
                    //TotalPrice = totalPrice,
                    ReservationDate = DateTime.Now,
                    PersonCount = vm.PersonCount,
                    Children = vm.Children

                };

                _context.Reservations.Add(reservation);
                _context.SaveChanges();
            
          

            return RedirectToAction("Index", "Home");   
        }



    }
}

