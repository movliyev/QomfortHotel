using Hotel_Managment.Domain.Entities;
using Hotel_Managment.Rersistance.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Managment.MVC_Qomfort_Project.ViewComponents.Home
{
    public class SlidePartial:ViewComponent
    {
        private readonly AppDbContext _context;

        public SlidePartial(AppDbContext context)
        {
           _context = context;
        }
        public async Task <IViewComponentResult> InvokeAsync()
        {
            List<Slide> slide = await _context.Slides.ToListAsync();
           
            return View(slide);  

        }
    }
}
