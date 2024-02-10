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
                .Include(r => r.Category).Take(4).ToList();
            List<Slide> slides = _context.Slides.ToList();
            List<Servicee> servicees = _context.Servisees.ToList();
            List<Gallery> galleries = _context.Galleries.ToList();
            List<Testimonial> testimonials = _context.Testimonials.ToList();
            HomeAbout homeabout = _context.HomeAbouts.FirstOrDefault();
            List<Blog> blogs = _context.Blogs.ToList();
            HomeVM vm = new HomeVM
            {
                HomeAbouts = homeabout,
                Blogs = blogs,
                Testimonials = testimonials,
                Servicees = servicees,
                Galleries = galleries,
                Slides = slides,
                Rooms = rooms
            };
            return View(vm);
        }


    }
}