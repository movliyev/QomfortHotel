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

        public ReservationController(AppDbContext context)
        {
            _context = context;
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
            return View();
        }
        [HttpPost]
        public IActionResult NewReservation(Reservation reservation)
        {
            //List<SelectListItem> list = new List<Select
            return View();
        }
    }
}
