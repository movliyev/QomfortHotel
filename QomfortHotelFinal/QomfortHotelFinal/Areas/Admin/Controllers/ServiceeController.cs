using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QomfortHotelFinal.Areas.Admin.ViewModels;
using QomfortHotelFinal.DAL;
using QomfortHotelFinal.Models;
using QomfortHotelFinal.Utilities.Exceptions;
using QomfortHotelFinal.Utilities.Extensions;

namespace QomfortHotelFinal.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class ServiceeController : Controller
    {
        private readonly AppDbContext _context;

        public ServiceeController(AppDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Admin,Memmber")]

        public async Task<IActionResult> Index(int page = 1)
        {
            if (page < 1) throw new WrongRequestException("The query is incorrect");

            int count = await _context.Servisees.CountAsync();
            List<Servicee> service = await _context.Servisees.Include(p=>p.RoomServicees).Skip((page - 1) * 3).Take(3).ToListAsync();
            PaginateVM<Servicee> pagvm = new PaginateVM<Servicee>
            {
                Items = service,
                TotalPage = Math.Ceiling((double)count / 3),
                CurrentPage = page,
            };

            return View(pagvm);
        }
        [Authorize(Roles = "Admin")]

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateServiceVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View();

            }
            bool result = _context.Servisees.Any(c => c.Name.Trim() == vm.Name.Trim());
            if (result)
            {
                ModelState.AddModelError("Name", "Bu adda Servicee movcuddur");
                return View();
            }

            Servicee Servicee = new Servicee
            {
                Name = vm.Name.Capitalize(),
                Icon = vm.Icon,
                Description = vm.Description
            };
            await _context.Servisees.AddAsync(Servicee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]

        //UPDATE 
        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) throw new WrongRequestException("The query is incorrect");
            Servicee Servicee = await _context.Servisees.FirstOrDefaultAsync(c => c.Id == id);
            if (Servicee == null) throw new NotFoundException("Services not found");
            UpdateServiceVM vm = new UpdateServiceVM
            {
                Name = Servicee.Name.Capitalize(),
                Icon = Servicee.Icon,
                Description = Servicee.Description  
                
            };

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateServiceVM Serviceevm)
        {
            if (id <= 0) throw new WrongRequestException("The query is incorrect");
            if (!ModelState.IsValid)
            {
                return View();
            }
            Servicee exsisted = await _context.Servisees.FirstOrDefaultAsync(c => c.Id == id);
            if (exsisted == null) throw new NotFoundException("Services not found");
            bool result = await _context.Servisees.AnyAsync(c => c.Name == Serviceevm.Name.Capitalize() && c.Id != id);
            if (result)
            {
                ModelState.AddModelError("Name", "Bu adda Servicee atriq movcuddur");

                return View();
            }
            exsisted.Name = Serviceevm.Name.Capitalize();
            exsisted.Icon = Serviceevm.Icon;    
            exsisted.Description = Serviceevm.Description;  
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin")]


        //DELETE

        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) throw new WrongRequestException("The query is incorrect");

            Servicee existed = await _context.Servisees.FirstOrDefaultAsync(c => c.Id == id);
            if (existed == null) return Json(new { status = 404 });
            try
            {

                _context.Servisees.Remove(existed);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                return Json(new { status = 500 });
            }

            return Json(new { status = 200 });
           
           
           
        }


    }
}
