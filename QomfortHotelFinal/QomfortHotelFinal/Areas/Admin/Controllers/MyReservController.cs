using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QomfortHotelFinal.DAL;
using QomfortHotelFinal.Models;

namespace QomfortHotelFinal.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    [Authorize(Roles = "Memmber")]
    public class MyReservController : Controller
    {
        private readonly AppDbContext _context;
        private readonly Room _room;
        private readonly UserManager<AppUser> _userman;

        public MyReservController(AppDbContext context,UserManager<AppUser> userman)
        {
            _context = context;
           _userman = userman;
        }
        [HttpGet]
        public async Task <IActionResult> MyOldReservation()
        {
            AppUser user = await _userman.FindByNameAsync(User.Identity.Name);
            if(user == null)return NotFound();
           
            var list = await _context.Reservations.Include(x=>x.Room).ThenInclude(x=>x.Category).Where(x=>x.AppUserId==user.Id).Where(x => x.DeparturDate < DateTime.Now).ToListAsync();
         
            
            return View(list);
        }
        [HttpGet]
        public async Task <IActionResult> MyCurrentReservation()
        {
            AppUser user = await _userman.FindByNameAsync(User.Identity.Name);
            if (user == null) return NotFound();

            var list = await _context.Reservations.Include(x => x.Room).ThenInclude(x => x.Category).Where(x => x.AppUserId == user.Id).Where(x => x.DeparturDate > DateTime.Now).ToListAsync();


            return View(list);
        }
       
    }
}
