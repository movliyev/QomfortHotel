using Hotel_Managment.Domain.Entities;
using Hotel_Managment.Rersistance.DAL;
using Hotel_Managment.Rersistance.Implementations.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Managment.MVC_Qomfort_Project.ViewComponents.Home
{
    public class RoomPartial:ViewComponent
    {
       
        private readonly AppDbContext _context;

        public RoomPartial(AppDbContext context)
        {
           
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(int key = 1)
        {
            List<Room> rooms = await _context.Rooms.Take(8).Include(p => p.RoomImages.Where(pi => pi.IsPrimary != true)).ToListAsync();

            switch (key)
            {
                case 1:
                    rooms = await _context.Rooms.OrderBy(p => p.Name).Take(8).Include(p => p.RoomImages.Where(pi => pi.IsPrimary != null)).ToListAsync();
                    break;
                case 2:
                    rooms = await _context.Rooms.OrderByDescending(p => p.Price).Take(8).Include(p => p.RoomImages.Where(pi => pi.IsPrimary != null)).ToListAsync();
                    break;
              
                default:
                    await _context.Rooms.Include(p => p.RoomImages.Where(pi => pi.IsPrimary != null)).ToListAsync();
                    break;
            }
            return View(rooms);
        }
    }
}
