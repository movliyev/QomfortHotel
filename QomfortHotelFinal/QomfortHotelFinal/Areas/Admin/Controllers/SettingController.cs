using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QomfortHotelFinal.Areas.Admin.ViewModels.Setting;
using QomfortHotelFinal.DAL;
using QomfortHotelFinal.Models;

namespace QomfortHotelFinal.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    [Authorize(Roles = "Admin,Memmber")]
    public class SettingController : Controller
    {
        private readonly AppDbContext _context;

        public SettingController(AppDbContext context)
        {
           _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Setting> settings = await _context.Settings.ToListAsync();
            return View(settings);
        }
      

        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) return BadRequest();
            Setting setting = await _context.Settings.FirstOrDefaultAsync(s => s.Id == id);
            if (setting == null) return NotFound();
            UpdateSettingVM vm = new UpdateSettingVM
            {
                Key = setting.Key,
                Value = setting.Value
            };
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateSettingVM svm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Setting exsisted = await _context.Settings.FirstOrDefaultAsync(s => s.Id == id);
            if (exsisted == null) return NotFound();
            bool result = await _context.Settings.AnyAsync(s => s.Key == svm.Key && s.Id != id);
            if (result)
            {
                ModelState.AddModelError("Name", "Bu adda Key atriq movcuddur");

                return View();
            }

            bool result2 = await _context.Settings.AnyAsync(s => s.Value == svm.Value && s.Id != id);
            if (result2)
            {
                ModelState.AddModelError("Name", "Bu adda Value atriq movcuddur");

                return View();
            }



            exsisted.Key = svm.Key;
            exsisted.Value = svm.Value;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
