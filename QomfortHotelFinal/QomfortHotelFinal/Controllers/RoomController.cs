
using Hangfire;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Memmber,Admin")]
        //\\\\\RESERVATION/////\\
        public async Task<IActionResult> Reserv(int id)
        {
            if (id == 0) throw new WrongRequestException("The query is incorrect");
            AppUser user = await _userManager.Users
           .Include(u => u.Reservations.Where(b => b.OrderId == null))
        .ThenInclude(bi => bi.Room)
        .FirstOrDefaultAsync(u => u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));


            Room room = await _context.Rooms.Include(x => x.Reservations.Where(x => x.RoomId == id))
                .Include(p => p.Category)
                .Include(p => p.RoomImages.Where(x => x.IsPrimary == true))
                .Include(p => p.RoomFacilities).ThenInclude(x => x.Facility)
                .Include(p => p.RoomServicees).ThenInclude(p => p.Servicee)
                .FirstOrDefaultAsync(x => x.Id == id);
            List<RoomImage> ri=await _context.RoomImages.ToListAsync();
            if (room == null) throw new NotFoundException("Room not found");

            var reservations = await _context.Reservations
           .Where(r => r.RoomId == id) 
           .ToListAsync();

            var reservationDates = reservations.SelectMany(r => Enumerable.Range(0, (r.DeparturDate - r.ArrivalDate).Days + 1)
                .Select(offset => r.ArrivalDate.AddDays(offset)))
                .ToList();
          
            RoomVM roomvm = new RoomVM
            {
                Room = room,
                ReservationDates = reservationDates,
                RoomaImage=ri
            };

            return View(roomvm);
        }
        [Authorize(Roles = "Memmber")]
        [HttpPost]
        public async Task<IActionResult> Reserv(int id, RoomVM vm)
        {
            if (id <= 0) throw new WrongRequestException("The query is incorrect");
            Room room = await _context.Rooms.Include(x => x.Reservations.Where(x => x.Id == id))
           .Include(p => p.Category)
           .Include(p => p.RoomImages.Where(x => x.IsPrimary == true))
           .Include(p => p.RoomFacilities).ThenInclude(x => x.Facility)
           .Include(p => p.RoomServicees).ThenInclude(p => p.Servicee)
           .FirstOrDefaultAsync(x => x.Id == id);
            if (room == null) throw new NotFoundException("Room not found");
            List<RoomImage> ri = await _context.RoomImages.ToListAsync();

            //reservations
            var reservations = await _context.Reservations
            .Where(r => r.RoomId == id) 
             .ToListAsync();

            // Reservation tarixleri
            var reservationDates = reservations.SelectMany(r => Enumerable.Range(0, (r.DeparturDate - r.ArrivalDate).Days + 1)
                .Select(offset => r.ArrivalDate.AddDays(offset)))
                .ToList();

            vm.ReservationDates = reservationDates;
            vm.Room = room;
            vm.RoomaImage = ri;
            if (!ModelState.IsValid)
            {
                vm.ReservationDates = reservationDates;
                vm.Room = room;
                vm.RoomaImage = ri;
                return View(vm);
            }
               
           
            if (vm.PersonCount + vm.Children > room.Capacity)
            {
                ModelState.AddModelError(string.Empty, "The total number of persons exceeds the room's capacity.");
                return View(vm);
            }

            var existingReservations = await _context.Reservations
      .Where(r => r.RoomId == id && r.Status == true)
      .ToListAsync();

            // Seçilen tarih aralığında başka bir rezervasyon var mı kontrol et
            foreach (var reserv in existingReservations)
            {
                if (vm.ArrivalDate <= reserv.DeparturDate.AddDays(1) && vm.DeparturDate >= reserv.ArrivalDate)
                {
                    if (DateTime.Now <= reserv.DeparturDate.AddDays(1))
                    {
                        ModelState.AddModelError(String.Empty, "There is another reservation in this date range.");
                        return View(vm);
                    }
                }
            }


            if (vm.ArrivalDate < DateTime.Today || vm.ArrivalDate > vm.DeparturDate || (vm.DeparturDate - vm.ArrivalDate).Days <= 0)
            {
                ModelState.AddModelError(String.Empty, "Invalid reservation dates.");
                return View(vm);
            }
            Reservation reservation = null;
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);

                reservation = new Reservation
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

               if (vm.DeparturDate > DateTime.Now && vm.ArrivalDate < DateTime.Now)
                {
                    room.Status = false;
                }
                else
                {
                    room.Status = true;
                }
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

            return RedirectToAction("Checkout", "Room", new {reservId=reservation.Id});
        }
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
        public async Task UpdateRoomStatusOnArrival(int roomId, DateTime arrivalDate)
        {
            var room = await _context.Rooms.FindAsync(roomId);
            if (room != null && arrivalDate < DateTime.Now)
            {
              
                room.Status = false;
                await _context.SaveChangesAsync();
            }
        }
        public async Task UpdateRoomStatus(int roomId)
        {
            var reservation = _context.Reservations.FirstOrDefault(r => r.RoomId == roomId && r.Status == true);

            if (reservation != null && reservation.DeparturDate < DateTime.Now)
            {
               
                reservation.Status = false;
                var room = _context.Rooms.Find(roomId);
                if (room != null)
                {
                    room.Status = true;
                }

                _context.SaveChanges();
            }
        }

        //\\\\\\\\\\\\CHECKOUT//\\\\\\\\\\\\
        public async Task<IActionResult> Checkout(int reservId)
        {
            AppUser user = await _userManager.Users
           .Include(u => u.Reservations.Where(b => b.OrderId == null))
           .ThenInclude(bi => bi.Room)
           .FirstOrDefaultAsync(u => u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));

            OrderVM orderVM = new OrderVM
            {

                AppUserId = user.Id,
                Reservations = user.Reservations.ToList(),
                TotalPrice = user.Reservations.Sum(r => (r.DeparturDate - r.ArrivalDate).Days * r.Room.Price)
            };

            return View(orderVM);
        }
        [HttpPost]
        public async Task<IActionResult> Checkout(int reservId,OrderVM ovm, string stripeEmail, string stripeToken)
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
            Reservation reservation = user.Reservations.FirstOrDefault(r => r.Id == reservId);
            decimal totalPrice = (reservation.DeparturDate-reservation.ArrivalDate).Days * reservation.Room.Price; // Ödenecek toplam tutar

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
            var serviceCharge = new ChargeService();
            Charge charge = serviceCharge.Create(optionsCharge);
            if (charge.Status != "succeeded")
            {
                ovm.Reservations = user.Reservations.ToList();
                ovm.TotalPrice = user.Reservations.Sum(r => (r.DeparturDate - r.ArrivalDate).Days * r.Room.Price);
                ModelState.AddModelError(String.Empty, "Odenishde problem var");
                return View(ovm);
            }
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            string body = @"
              <p>Your order succesfully placed:</p>
             <table border=""1"">
               <thead>
                   <tr>
                       <th> Name </th>                       
                       <th> ArrivalDate </th>
                       <th> Departurdate </th>
                       <th> Price </th>
                   </tr>
               </thead>
               <tbody>";
            foreach (var item in order.Reservations)
            {
                body += @$" <tr>
                        <td>{item.Room.Name}</td>                     
                        <td>{item.ArrivalDate.ToShortDateString()}</td>
                           <td>{item.DeparturDate.ToShortDateString()}</td>
                        <td >{(item.Room.Price) * (item.DeparturDate - item.ArrivalDate).Days}</td>
                    </tr>";

            }
            body += @" </tbody>
             </table>";



            await _emailService.SendEmailAsync(user.Email, "Your Order", body, true);
            return RedirectToAction("Index", "Home");

        }





        public async Task<IActionResult> RoomList(int id,string? search,int? order,int? categoryId,int? serviceId, int page = 1)
        {

            if (page < 1) throw new WrongRequestException("The query is incorrect");

            int count = await _context.Rooms.CountAsync();
            IQueryable<Room> query =_context.Rooms.Include(x=>x.RoomImages).AsQueryable();
            switch (order)
            {
                case 1:
                    query=query.OrderBy(x=>x.Name);
                    break;
                case 2:
                    query = query.OrderBy(x => x.Price);
                    break;
                case 3:
                    query = query.OrderBy(x => x.Capacity);
                    break;
                case 4:
                    query = query.OrderBy(x => x.Size);
                    break;
                case 5:
                    query = query.OrderByDescending(x => x.BathRoom);
                    break;
                case 6:
                    query = query.OrderByDescending(x => x.Bed);
                    break;
              
                case 7:
                    query = query.OrderByDescending(x => x.Id);
                    break;

            }
            if (!String.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.Name.ToLower().Contains(search.ToLower()));
            }
            if(categoryId != null)
            {
                query=query.Where(x=>x.CategoryId==categoryId);
            }
            if (serviceId != null)
            {
                query = query.Where(x => x.RoomServicees.Any(rs => rs.ServiceeId == serviceId));
            }
            PaginateVM<Room> vm = new PaginateVM<Room>
            {
                Services = await _context.Servisees.OrderByDescending(x => x.Id).Include(x => x.RoomServicees).ThenInclude(x => x.Room).Take(8).ToListAsync(),
                Blogs = await _context.Blogs.OrderByDescending(x => x.Id).Take(5).ToListAsync(),
                Items = await query.Skip((page - 1) * 5).Take(5).ToListAsync(),
                Categories = await _context.Categories.Include(x => x.Rooms).ToListAsync(),
                Orrder = order,
                Search = search,
                CategoryId = categoryId,
                ServiceId = serviceId,
                Galleries = await _context.Galleries.OrderByDescending(x=>x.Id).Take(8).ToListAsync(),
                TotalPage = Math.Ceiling((double)count / 5),
                CurrentPage = page,
            };
            return View(vm);
        }


    }
}







////Departurdate de room statusu true olur
//public async Task UpdateRoomStatus(int roomId)
//{
//    var reservation = _context.Reservations.FirstOrDefault(r => r.RoomId == roomId && r.Status == true);

//    if (reservation != null && reservation.DeparturDate < DateTime.Now)
//    {
//        // Rezervasyonun bitiş tarihi geçmişse, rezervasyonu iptal et ve odayı yeniden müsait yap
//        reservation.Status = false;
//        var room = _context.Rooms.Find(roomId);
//        if (room != null)
//        {
//            room.Status = true;
//        }

//        _context.SaveChanges();
//    }
//}
//public async Task UpdateRoomStatusArrival(int roomId)
//{
//    // Odanın rezervasyonunu kontrol et
//    var reservation = _context.Reservations.FirstOrDefault(r => r.RoomId == roomId && r.Status == true);

//    // Eğer rezervasyon varsa ve varış tarihi şu anki tarihten büyükse
//    if (reservation != null && reservation.ArrivalDate > DateTime.Now)
//    {
//        // Rezervasyonun durumunu güncelle (varış tarihi gelmiş)
//        reservation.Status = true;

//        // Oda durumunu güncelle (işgal edilmiş)
//        var room = _context.Rooms.Find(roomId);
//        if (room != null)
//        {
//            room.Status = true;
//        }

//        // Değişiklikleri veritabanına kaydet
//        await _context.SaveChangesAsync();
//    }
//}




//var arrival = vm.ArrivalDate;
//var jobId1 = BackgroundJob.Schedule(() => UpdateRoomStatusArrival(room.Id), arrival);
//var endTime = vm.DeparturDate;
//var jobId = BackgroundJob.Schedule(() => UpdateRoomStatus(room.Id), endTime);