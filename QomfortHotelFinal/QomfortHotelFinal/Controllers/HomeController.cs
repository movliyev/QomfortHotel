using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QomfortHotelFinal.DAL;
using QomfortHotelFinal.Models;
using QomfortHotelFinal.ViewModels;
using System.Diagnostics;

namespace QomfortHotelFinal.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {


        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {

            List<Room> rooms = _context.Rooms.Include(r => r.RoomImages.Where(x => x.IsPrimary == true))
                .Include(r => r.Category).Include(x=>x.Reservations.Where(x=>x.Status==true)).Take(4).ToList();
            List<Slide> slides = _context.Slides.ToList();
            List<Servicee> servicees = _context.Servisees.Take(8).ToList();
            List<Gallery> galleries = _context.Galleries.Take(8).ToList();
            List<Comment> comments =  _context.Comments.Include(x => x.Blog).Include(x => x.AppUser).OrderByDescending(x => x.Id).ToList();
            HomeAbout homeabout = _context.HomeAbouts.FirstOrDefault();
            List<Blog> blogs = _context.Blogs.OrderByDescending(x=>x.Id).Take(3).ToList();
            HomeVM vm = new HomeVM
            {
                HomeAbouts = homeabout,
                Comments=comments,
                Blogs = blogs,
                Servicees = servicees,
                Galleries = galleries,
                Slides = slides,
                Rooms = rooms
            };
            return View(vm);
        }
        [HttpPost]
        public IActionResult Index(HomeVM vm)
        {
            if (vm.ArrivalDate == null || vm.DeparturDate == null || vm.Adult <= 0 || vm.Adult == null|| vm.Children <= 0 || vm.Children == null)
            {
                return View();
            }

            if (vm.ArrivalDate >= vm.DeparturDate)
            {
                return View();
            }

            var roomsBooked = from b in _context.Reservations
                              where
                              ((vm.ArrivalDate >= b.ArrivalDate) && (vm.ArrivalDate <= b.DeparturDate)) ||
                              ((vm.DeparturDate >= b.ArrivalDate) && (vm.DeparturDate <= b.DeparturDate)) ||
                              ((vm.ArrivalDate <= b.ArrivalDate) && (vm.DeparturDate >= b.DeparturDate) && (vm.DeparturDate <= b.DeparturDate)) ||
                              ((vm.ArrivalDate >= b.ArrivalDate) && (vm.ArrivalDate <= b.DeparturDate) && (vm.DeparturDate >= b.DeparturDate)) ||
                              ((vm.ArrivalDate <= b.DeparturDate) && (vm.DeparturDate >= b.DeparturDate))
                              select b;

            var availableRooms = _context.Rooms.Where(x=>x.Status==true).Where(r => !roomsBooked.Any(b => b.RoomId == r.Id))
                .Include(x => x.Category).ToList();
            int max = vm.Adult + vm.Children;
            foreach (var item in availableRooms)
            {
                if (item.Capacity >= max)
                {
                    vm.Room.Add(item);
                }
            }

            return View(vm);
        }

        public IActionResult ErrorPage(string error="Xeta Bash Verdi")
        {
            return View(model:error);
        }                                       


    }
}