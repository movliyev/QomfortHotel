using Hangfire;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QomfortHotelFinal.DAL;
using QomfortHotelFinal.Models;
using QomfortHotelFinal.ViewModels;
using System.Security.Claims;

namespace QomfortHotelFinal.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public ContactController(AppDbContext context,UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task <IActionResult> Index()
        {
            AppUser user = await _userManager.Users
             .Include(x=>x.Messagess)
           .FirstOrDefaultAsync(u => u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));
            Contact contact = await _context.Contacts.FirstOrDefaultAsync();
            if(contact==null) return NotFound();
            ContactVM vm = new ContactVM
            {
                Contact = contact,
            };
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Index(ContactVM vm)
        {
            Contact contact = await _context.Contacts.FirstOrDefaultAsync();
            if (contact == null) return NotFound();
            vm.Contact = contact;
            if(!ModelState.IsValid) return View(vm);
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);

                Message message = new Message
                {
                  MessageDate=DateTime.Now,
                  Messages=vm.Messages,
                    Rate = vm.Rating,
                    Subject=vm.Subject,
                };

                await _context.Messages.AddAsync(message);
                await _context.SaveChangesAsync();
              
            }
            else
            {

                return RedirectToAction("Login", "Account");
            }

            return RedirectToAction("Index", "Contact");
        }
    }
}
