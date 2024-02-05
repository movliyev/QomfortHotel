using Hotel_Managment.Domain.Entities;
using Hotel_Managment.Rersistance.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Managment.MVC_Qomfort_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
           _context = context;
        }
        public IActionResult Index()
        {
            List<Room> r=_context.Rooms.Include(r=>r.RoomImages.Where(x => x.IsPrimary==true))
                .Include(r=>r.Category).Take(4).ToList();
            return View(r);
        }

      
    }
}