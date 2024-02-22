using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QomfortHotelFinal.Areas.Admin.ViewModels;
using QomfortHotelFinal.Areas.Admin.ViewModels.Contact;
using QomfortHotelFinal.DAL;
using QomfortHotelFinal.Models;
using QomfortHotelFinal.Utilities.Extensions;

namespace QomfortHotelFinal.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    [Authorize(Roles = "Admin")]
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ContactController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
           _env = env;
        }
        public async Task<IActionResult> Index()
        {

            List<Contact> Contact = await _context.Contacts.ToListAsync();

            return View(Contact);
        }


        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) return BadRequest();
            Contact exsist = await _context.Contacts.FirstOrDefaultAsync(s => s.Id == id);
            if (exsist == null) return NotFound();

            UpdateContactVM vm = new UpdateContactVM
            {
              HoverImage = exsist.HoverImage,
              Title = exsist.Title,
              Email = exsist.Email,
              PhoneNumber = exsist.PhoneNumber,
              Location = exsist.Location,
            };

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateContactVM vm)
        {
            if(id<=0) return BadRequest();  
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            if (!vm.Email.CheckEmail())
            {
                ModelState.AddModelError("Email", "Email uslubu yanlisdir");
                return View(vm);
            }
            Contact exsist = await _context.Contacts.FirstOrDefaultAsync(s => s.Id == id);
            if (exsist == null) return NotFound();

            if (vm.Photo is not null)
            {
                if (!vm.Photo.ValidateType("image/"))
                {
                    ModelState.AddModelError("Photo", "File tipi uyqun deyil");
                    return View(vm);
                }

                if (!vm.Photo.ValidateSize(2 * 1024))
                {
                    ModelState.AddModelError("Photo", "File olcusu 2-mb den boyuk olmamalidir");
                    return View(vm);

                }
                string filename = await vm.Photo.CreateFileAsync(_env.WebRootPath, "assets", "images", "background");
                exsist.HoverImage.DeleteFile(_env.WebRootPath, "assets", "images", "background");
                exsist.HoverImage = filename;
            }

            exsist.Title = vm.Title.Capitalize();
            exsist.Email = vm.Email;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
