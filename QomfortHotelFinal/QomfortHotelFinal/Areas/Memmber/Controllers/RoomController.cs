using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QomfortHotelFinal.DAL;
using QomfortHotelFinal.Models;

namespace QomfortHotelFinal.Areas.Memmber.Controllers
{
    [Area("Memmber")]
    public class RoomController : Controller
    {
        private readonly AppDbContext _context;

        public RoomController(AppDbContext context)
        {
            _context = context;
        }
        public async Task <IActionResult> Index()
        {
            List<Room>rooms= await _context.Rooms.Include(x=>x.Category).Include(x=>x.RoomImages)
                .Include(x=>x.RoomFacilities).ThenInclude(x=>x.Facility)
                .ToListAsync();
            return View(rooms);
        }
    }
}
