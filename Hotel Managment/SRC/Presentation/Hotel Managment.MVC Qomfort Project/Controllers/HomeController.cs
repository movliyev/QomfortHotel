using Hotel_Managment.Domain.Entities;
using Hotel_Managment.MVC_Qomfort_Project.ViewModels;
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

            List<Room> rooms=_context.Rooms.Include(r=>r.RoomImages.Where(x => x.IsPrimary==true))
                .Include(r=>r.Category).Take(4).ToList();
            List<Slide> slides = _context.Slides.ToList();
            List<Servicee> servicees = _context.Servicees.ToList();
            List<Gallery> galleries = _context.Galleries.ToList();
            List<Testimonial> testimonials = _context.Testimonials.ToList();
            HomeAbout homeabout = _context.HomeAbouts.FirstOrDefault();
            List<Blog> blogs = _context.Blogs.ToList();
            HomeVM vm = new HomeVM
            {
                HomeAbouts=homeabout,
                Blogs = blogs,
                Testimonials = testimonials,
                Servicees = servicees,
                Galleries = galleries,
                Slides = slides,
                Rooms=rooms
            };
            return View(vm);
        }

      
    }
}