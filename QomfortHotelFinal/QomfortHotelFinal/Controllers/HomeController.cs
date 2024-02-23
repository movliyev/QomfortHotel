using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QomfortHotelFinal.DAL;
using QomfortHotelFinal.Models;
using QomfortHotelFinal.ViewModels;

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
        public IActionResult Index(int? serviceid)
        {

            List<Room> rooms = _context.Rooms.Include(r => r.RoomImages.Where(x => x.IsPrimary == true))
                .Include(r => r.Category).Include(x=>x.Reservations.Where(x=>x.Status==true)).Take(4).ToList();
            List<Slide> slides = _context.Slides.ToList();
            List<Servicee> servicees = _context.Servisees.Include(x=>x.RoomServicees).Take(8).ToList();
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
      

        public IActionResult ErrorPage(string error="Xeta Bash Verdi")
        {
            return View(model:error);
        }                                       


    }
}