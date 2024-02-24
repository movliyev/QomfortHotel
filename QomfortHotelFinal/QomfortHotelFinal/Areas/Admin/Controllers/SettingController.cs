using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QomfortHotelFinal.Areas.Admin.ViewModels.Setting;
using QomfortHotelFinal.DAL;
using QomfortHotelFinal.Models;
using QomfortHotelFinal.Utilities.Exceptions;

namespace QomfortHotelFinal.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    [Authorize(Roles = "Admin")]
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
            if (id <= 0) throw new WrongRequestException("The query is incorrect");
            Setting setting = await _context.Settings.FirstOrDefaultAsync(s => s.Id == id);
            if (setting == null) throw new NotFoundException("setting not found");
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
            if (id <= 0) throw new WrongRequestException("The query is incorrect");

            if (!ModelState.IsValid)
            {
                return View();
            }
            Setting exsisted = await _context.Settings.FirstOrDefaultAsync(s => s.Id == id);
            if (exsisted == null) throw new NotFoundException("Setting not found");
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
