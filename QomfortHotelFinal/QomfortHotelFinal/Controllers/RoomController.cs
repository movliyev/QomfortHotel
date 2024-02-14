using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QomfortHotelFinal.DAL;
using QomfortHotelFinal.Models;
using QomfortHotelFinal.ViewModels;

namespace QomfortHotelFinal.Controllers
{
    public class RoomController : Controller
    {
        private readonly AppDbContext _context;

        public RoomController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Detail(int id)
        {
            if (id == 0) return BadRequest();

            List<Room> room = await _context.Rooms.Include(p => p.RoomImages)
                .Include(p => p.RoomFacilities).ThenInclude(x => x.Facility)
                .Include(p => p.RoomServicees).ThenInclude(p=>p.Servicee)
                .Include(r => r.Category).ToListAsync();
            Room room1 = await _context.Rooms
                .Include(p => p.Category)
                .Include(p => p.RoomImages)
                .Include(p => p.RoomFacilities).ThenInclude(x => x.Facility)
                .Include(p => p.RoomServicees).ThenInclude(p => p.Servicee)
                .FirstOrDefaultAsync(x => x.Id == id);
            Reservation rvm = _context.Reservations.FirstOrDefault();

            if (room1 == null) return NotFound();


            RoomVM roomvm = new RoomVM
            {
                Reservation = rvm,
                Room = room1,
                Rooms = room

            };

            return View(roomvm);
        }
        public async Task<IActionResult> RoomList()
        {

            List<Room> room = await _context.Rooms.Include(p => p.RoomImages).Include(r => r.Category).ToListAsync();
            return View(room);
        }

        //public async Task<IActionResult> NewReservation()
        //{

        //}
    }
}
