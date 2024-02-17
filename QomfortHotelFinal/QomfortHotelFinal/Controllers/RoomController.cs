
using Hangfire;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QomfortHotelFinal.DAL;
using QomfortHotelFinal.Models;
using QomfortHotelFinal.Utilities.Exceptions;
using QomfortHotelFinal.ViewModels;
using QomfortHotelFinal.ViewModels.Reservationvm;
using System.Security.Claims;

namespace QomfortHotelFinal.Controllers
{
    public class RoomController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
       
        public RoomController(AppDbContext context,UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Reserv(int id)
        {
            AppUser user = await _userManager.GetUserAsync(User);
            if (id == 0) throw new WrongRequestException("The query is incorrect");
                     
            Room room = await _context.Rooms.Include(x=>x.Reservations.Where(x=>x.Id==id))
                .Include(p => p.Category)
                .Include(p => p.RoomImages.Where(x=>x.IsPrimary==true))
                .Include(p => p.RoomFacilities).ThenInclude(x => x.Facility)
                .Include(p => p.RoomServicees).ThenInclude(p => p.Servicee)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (room == null) throw new NotFoundException("Room not found");

            var reservations = await _context.Reservations
       .Where(r => r.RoomId == id) // Odanın mevcut ve aktif rezervasyonlarını al
       .ToListAsync();

            // Oda rezervasyon tarihleri
            var reservationDates = reservations.SelectMany(r => Enumerable.Range(0, (r.DeparturDate - r.ArrivalDate).Days + 1)
                .Select(offset => r.ArrivalDate.AddDays(offset)))
                .ToList();

            



            RoomVM roomvm = new RoomVM
            {
                Room=room,
                ReservationDates = reservationDates,
               
                
            };

            return View(roomvm);
        }

        [HttpPost]
        public async Task<IActionResult> Reserv(int id,RoomVM vm)
        {
            Room room = await _context.Rooms.Include(x => x.Reservations.Where(x => x.Id == id))
            .Include(p => p.Category)
            .Include(p => p.RoomImages.Where(x => x.IsPrimary == true))
            .Include(p => p.RoomFacilities).ThenInclude(x => x.Facility)
            .Include(p => p.RoomServicees).ThenInclude(p => p.Servicee)
            .FirstOrDefaultAsync(x => x.Id == id);
         
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

         
            var existingReservations = await _context.Reservations
       .Where(r => r.RoomId == id && r.Status == true) // Odanın mevcut ve aktif rezervasyonlarını al
       .ToListAsync();

            // Seçilen tarih aralığında başka bir rezervasyon var mı kontrol et
            foreach (var reservation in existingReservations)
            {
                if (vm.ArrivalDate <= reservation.DeparturDate && vm.DeparturDate >= reservation.ArrivalDate)
                {
                    // Rezervasyonun bitiş tarihi kontrol ediliyor
                    if (DateTime.Now <= reservation.DeparturDate)
                    {
                        ModelState.AddModelError(String.Empty, "Belirtilen tarih aralığında başka bir rezervasyon bulunmaktadır.");
                        return View(vm);
                    }
                }
            }
            if (room == null)
            {
                ModelState.AddModelError(String.Empty, "The selected room could not be found.");
                return View(vm);
            }
           
         
            if (vm.ArrivalDate < DateTime.Today || vm.ArrivalDate > vm.DeparturDate)
            {
                ModelState.AddModelError(String.Empty, "Invalid reservation dates.");
                return View(vm);
            }
            int totalDays = (vm.DeparturDate - vm.ArrivalDate).Days;
            if (totalDays <= 0)
            {
                ModelState.AddModelError(String.Empty, "Geçersiz rezervasyon tarihleri.");
                return View(vm);
            }
            
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);

                Reservation reservation = new Reservation
                {
                    Status = true,
                    ArrivalDate = vm.ArrivalDate,
                    DeparturDate = vm.DeparturDate,
                    ReservationDate = DateTime.Now,
                    RoomId = room.Id,
                    AppUserId = user.Id, 
                    Children = vm.Children,
                    PersonCount = vm.PersonCount,
                    
                };
                room.Status = false;
                await _context.Reservations.AddAsync(reservation);
                await _context.SaveChangesAsync();
                var endTime = vm.DeparturDate;
                var jobId = BackgroundJob.Schedule(() => UpdateRoomStatus(room.Id), endTime);
            }
            else
            {
                
                return RedirectToAction("Login", "Account");
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task UpdateRoomStatus(int roomId)
        {
            var reservation = _context.Reservations.FirstOrDefault(r => r.RoomId == roomId && r.Status == true);

            if (reservation != null && reservation.DeparturDate < DateTime.Now)
            {
                // Rezervasyonun bitiş tarihi geçmişse, rezervasyonu iptal et ve odayı yeniden müsait yap
                reservation.Status = false;
                var room = _context.Rooms.Find(roomId);
                if (room != null)
                {
                    room.Status = true;
                }

                _context.SaveChanges();
            }
        }
        
        public async Task<IActionResult> RoomList()
        {
           
            List<Room> room = await _context.Rooms.Include(x=>x.Reservations).Include(p => p.RoomImages).Include(r => r.Category).ToListAsync();
            return View(room);
        }

     
    }
}
