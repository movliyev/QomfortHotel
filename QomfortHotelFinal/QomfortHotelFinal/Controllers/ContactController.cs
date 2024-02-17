using Microsoft.AspNetCore.Mvc;
using QomfortHotelFinal.DAL;
using QomfortHotelFinal.Models;
using QomfortHotelFinal.ViewModels.Contact;

namespace QomfortHotelFinal.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;

        public ContactController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(ContactVM vm)
        {
            if(!ModelState.IsValid)
            {
                return View(vm);
            }
            await _context.Contacts.AddAsync(new Contact
            {
                Email = vm.Email,
                FullName = vm.FullName,
                MessageStatus = true,
                Message = vm.Message,
                Subject = vm.Subject,
                MessageDate = DateTime.Now,
            });
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
