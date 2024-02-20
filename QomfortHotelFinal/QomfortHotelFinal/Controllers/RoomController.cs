
using Hangfire;
using MailKit.Search;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QomfortHotelFinal.Abstractions.MailService;
using QomfortHotelFinal.DAL;
using QomfortHotelFinal.Models;
using QomfortHotelFinal.Utilities.Exceptions;
using QomfortHotelFinal.ViewModels;
using Stripe;
using System.Security.Claims;

namespace QomfortHotelFinal.Controllers
{
    public class RoomController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailService _emailService;

        public RoomController(AppDbContext context, UserManager<AppUser> userManager, IEmailService emailService)
        {
            _context = context;
            _userManager = userManager;
            _emailService = emailService;
        }
        public async Task<IActionResult> Reserv(int id)
        {
            if (id == 0) throw new WrongRequestException("The query is incorrect");
            AppUser user = await _userManager.Users
           .Include(u => u.Reservations.Where(b => b.OrderId == null))
        .ThenInclude(bi => bi.Room)
        .FirstOrDefaultAsync(u => u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));
            //if (user == null) throw new NotFoundException("User not found");


            Room room = await _context.Rooms.Include(x => x.Reservations.Where(x => x.RoomId ==id))
                .Include(p => p.Category)
                .Include(p => p.RoomImages.Where(x => x.IsPrimary == true))
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
                Room = room,
                ReservationDates = reservationDates,

            };

            return View(roomvm);
        }

        [HttpPost]
        public async Task<IActionResult> Reserv(int id, RoomVM vm)
        {
            if (id <= 0) return BadRequest();
            Room room = await _context.Rooms.Include(x => x.Reservations.Where(x => x.Id == id))
           .Include(p => p.Category)
           .Include(p => p.RoomImages.Where(x => x.IsPrimary == true))
           .Include(p => p.RoomFacilities).ThenInclude(x => x.Facility)
           .Include(p => p.RoomServicees).ThenInclude(p => p.Servicee)
           .FirstOrDefaultAsync(x => x.Id == id);
            if (room == null)
            {   

                ModelState.AddModelError(String.Empty, "The selected room could not be found.");
                return View(vm);
            }
            var reservations = await _context.Reservations
            .Where(r => r.RoomId == id) // Odanın mevcut ve aktif rezervasyonlarını al
             .ToListAsync();

            // Oda rezervasyon tarihleri
            var reservationDates = reservations.SelectMany(r => Enumerable.Range(0, (r.DeparturDate - r.ArrivalDate).Days + 1)
                .Select(offset => r.ArrivalDate.AddDays(offset)))
                .ToList();
            vm.ReservationDates = reservationDates;
            vm.Room = room;
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(String.Empty, "The selected room could not be found.");
                return View(vm);
            }

            if (vm.PersonCount + vm.Children > room.Capacity)
            {
                
                ModelState.AddModelError(string.Empty, "The total number of persons exceeds the room's capacity.");
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



            if (vm.ArrivalDate < DateTime.Today || vm.ArrivalDate > vm.DeparturDate)
            {
               
                ModelState.AddModelError(String.Empty, "Invalid reservation dates.");
                return View(vm);
            }
            // Toplam gün sayısını hesaplayın
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
               
                      
                await _context.Reservations.AddAsync(reservation);
                await _context.SaveChangesAsync();
                await UpdateRoomStatusOnArrival(room.Id, vm.ArrivalDate);
                var endTime = vm.DeparturDate;
                var jobId = BackgroundJob.Schedule(() => UpdateRoomStatus(room.Id), endTime);

            }
            else
            {

                return RedirectToAction("Login", "Account");
            }

            return RedirectToAction("Checkout", "Room");
        }
        // Reservation Databazadan silinerse room statusu true olsun
        public async Task<IActionResult> CancelReservation(int reservationId)
        {
            var reservation = await _context.Reservations.FindAsync(reservationId);
            if (reservation == null)
            {
                return NotFound();
            }

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();

            // Rezervasyon silindiğinde oda durumunu kontrol etmek üzere bir arka plan işi planla
            var jobId = BackgroundJob.Enqueue(() => UpdateRoomStatusOnReservationCancellation(reservation.RoomId));

            return RedirectToAction("Index");
        }

        public async Task UpdateRoomStatusOnReservationCancellation(int roomId)
        {
            var room = await _context.Rooms.FindAsync(roomId);
            if (room != null)
            {
                // Rezervasyonlar kontrol edilerek odanın durumu güncellenir
                var activeReservation = await _context.Reservations.FirstOrDefaultAsync(r => r.RoomId == roomId && r.Status == true);
                room.Status = (activeReservation != null);
                await _context.SaveChangesAsync();
            }
        }
        // Arrivadate de room statusu false olur
        public async Task UpdateRoomStatusOnArrival(int roomId, DateTime arrivalDate)
        {
            var room = await _context.Rooms.FindAsync(roomId);
            if (room != null && arrivalDate < DateTime.Now)
            {
                // Gelen tarih geçmişse, oda durumunu false yap
                room.Status = false;
                await _context.SaveChangesAsync();
            }
        }
        //Departurdate de room statusu true olur
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

        public async Task<IActionResult> Checkout()
        {
            AppUser user = await _userManager.Users
        .Include(u => u.Reservations.Where(b => b.OrderId == null))
        .ThenInclude(bi => bi.Room)
        .FirstOrDefaultAsync(u => u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));

            OrderVM orderVM = new OrderVM
            {
                
                AppUserId=user.Id,
                Reservations = user.Reservations.ToList(),
                TotalPrice = user.Reservations.Sum(r => (r.DeparturDate - r.ArrivalDate).Days * r.Room.Price)
            };

            return View(orderVM);
        }
        [HttpPost]
        public async Task<IActionResult> Checkout(OrderVM ovm,string stripeEmail,string stripeToken)
        {
            AppUser user = await _userManager.Users
        .Include(u => u.Reservations.Where(b => b.OrderId == null))
        .ThenInclude(bi => bi.Room)
        .FirstOrDefaultAsync(u => u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (!ModelState.IsValid)
            {
                ovm.Reservations = user.Reservations.ToList();
                ovm.TotalPrice = user.Reservations.Sum(r => (r.DeparturDate - r.ArrivalDate).Days * r.Room.Price);
                return View(ovm);
            }

            decimal totalPrice = ovm.TotalPrice; // Ödenecek toplam tutar
           
            // Siparişi oluştur
            Order order = new Order
            {
                Status = null,
                AppUserId = user.Id,
                PurchasedAt = DateTime.Now,
                Reservations = user.Reservations.ToList(),
                TotalPrice = totalPrice
            };
            var optionCust = new CustomerCreateOptions
            {
                Email = stripeEmail,
                Name = user.Name + " " + user.Surname,
                Phone = user.PhoneNumber
            };
            var serviceCust = new CustomerService();
            Customer customer = serviceCust.Create(optionCust);

            totalPrice = totalPrice * 100;
            var optionsCharge = new ChargeCreateOptions
            {

                Amount = (long)totalPrice,
                Currency = "USD",
                Description = "Room Selling amount",
                Source = stripeToken,
                ReceiptEmail = stripeEmail


            };
            //var serviceCharge = new ChargeService();
            //Charge charge = serviceCharge.Create(optionsCharge);
            //if (charge.Status != "succeeded")
            //{
            //    ViewBag.Reservations = item;
            //    ModelState.AddModelError("Room", "Odenishde problem var");
            //    return View();
            //}
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            string body = @"
              <p>Your order succesfully placed:</p>
             <table border=""1"">
               <thead>
                   <tr>
                       <th> Name </th>
                       <th> Price </th>
                   </tr>
               </thead>
               <tbody>";
            foreach (var item in order.Reservations)
            {
                body += @$" <tr>
                        <td>{item.Room.Name}</td>
                        <td >{(item.Room.Price)*(item.DeparturDate-item.ArrivalDate).Days}</td>
                    </tr>";

            }
            body += @" </tbody>
             </table>";



            await _emailService.SendEmailAsync(user.Email, "Your Order", body, true);
            return RedirectToAction("Index", "Home");

        }

        public async Task<IActionResult> RoomList()
        {
            List<Room> room = await _context.Rooms.Include(x => x.Reservations).Include(p => p.RoomImages).Include(r => r.Category).ToListAsync();
            return View(room);
        }


    }
}
