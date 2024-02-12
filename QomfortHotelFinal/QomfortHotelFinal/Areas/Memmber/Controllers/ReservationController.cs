using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QomfortHotelFinal.DAL;
using QomfortHotelFinal.Models;

namespace QomfortHotelFinal.Areas.Memmber.Controllers
{
    [Area("Memmber")]
    public class ReservationController : Controller
    {
        private readonly AppDbContext _context;
        private readonly Room _room;
        private readonly UserManager<AppUser> _userman;

        public ReservationController(AppDbContext context,Room room,UserManager<AppUser> userman)
        {
            _context = context;
            _room = room;
           _userman = userman;
        }
        [HttpGet]
        public IActionResult MyOldReservation()
        {
            return View();
        }
        [HttpGet]
        public IActionResult MyCurrentReservation()
        {
            return View();
        }
        [HttpGet]
        public IActionResult NewReservation()
        {

           
            List<SelectListItem> list = (from x in _context.Rooms.ToList()
                                         select new SelectListItem  
                                         {
                                           Text=x.Name.ToString(),
                                           Value=x.ToString()
                                            
                                         } ).ToList();
            ViewBag.v = list;
            return View();
        }
        [HttpPost]
        public IActionResult NewReservation(Reservation reservation)
        {
            var result = _userman.FindByNameAsync(User.Identity.Name);
            var room = _context.Rooms.ToList();
           
                reservation.AppUserId = 3;

            _context.Reservations.Add(reservation);
            return RedirectToAction("MyCurrentReservation");   
        }
    }
}
