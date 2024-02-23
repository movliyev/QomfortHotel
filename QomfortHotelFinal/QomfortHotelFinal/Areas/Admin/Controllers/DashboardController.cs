using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QomfortHotelFinal.Areas.Admin.ViewModels.Dashboard;
using QomfortHotelFinal.DAL;
using QomfortHotelFinal.Models;

namespace QomfortHotelFinal.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    [Authorize(Roles = "Admin,Memmber,Blogger")]
    public class DashboardController : Controller
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {

            List<Room> room=_context.Rooms.Include(x=>x.RoomImages.Where(x=>x.IsPrimary==true)).Include(x=>x.Category).Include(x=>x.Category).Include(x=>x.RoomFacilities).ThenInclude(x=>x.Facility).OrderByDescending(x=>x.Id).Take(5).ToList();
            List<Message>  message =_context.Messages.Include(x=>x.AppUser).ToList();
            var slides=_context.Slides.ToList();
            var reservations=_context.Reservations.ToList();
            var Service=_context.Servisees.ToList();
            DashboardVM vm = new DashboardVM
            {
                Rooms=room,
                Reservations=reservations,
                Slides=slides,
                Messages=message,   
                    Services=Service,
            };
            return View(vm);
        }   
    }
}
