using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QomfortHotelFinal.Areas.Admin.ViewModels;
using QomfortHotelFinal.DAL;
using QomfortHotelFinal.Models;
using QomfortHotelFinal.Utilities.Exceptions;

namespace QomfortHotelFinal.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    
    public class FacilityController : Controller
    {
        private readonly AppDbContext _context;

        public FacilityController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin,Memmber,Blogger")]
        public async Task<IActionResult> Index(int page = 1)
        {
            if (page < 1) throw new WrongRequestException("The query is incorrect");

            int count = await _context.Facilities.CountAsync();
            List<Facility> Facilitys = await _context.Facilities.Skip((page - 1) * 3).Take(3)
                .Include(t => t.RoomFacilities).ToListAsync();
            PaginateVM<Facility> pagvm = new PaginateVM<Facility>
            {
                Items = Facilitys,
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
        public async Task<IActionResult> Create(CreateFacilityVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View();

            }
            bool result = _context.Facilities.Any(c => c.Name.Trim() == vm.Name.Trim());
            if (result)
            {
                ModelState.AddModelError("Name", "Bu adda Facility movcuddur");
                return View();
            }

            Facility Facility = new Facility
            {
                Name = vm.Name
            };


            await _context.Facilities.AddAsync(Facility);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]

        //UPDATE 
        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) throw new WrongRequestException("The query is incorrect");
            Facility Facility = await _context.Facilities.FirstOrDefaultAsync(t => t.Id == id);
            if (Facility == null) throw new NotFoundException("Facility not found");

            UpdateFacilityVM vm = new UpdateFacilityVM
           {
               Name=Facility.Name,
           };

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateFacilityVM vm)
        {
            if (id <= 0) throw new WrongRequestException("The query is incorrect");

            if (!ModelState.IsValid)
            {
                return View();
            }
            Facility exsisted = await _context.Facilities.FirstOrDefaultAsync(c => c.Id == id);
            if (exsisted == null)throw new NotFoundException("Facility not found");
            bool result = await _context.Facilities.AnyAsync(c => c.Name == vm.Name && c.Id != id);
            if (result)
            {
                ModelState.AddModelError("Name", "Bu adda Facility atriq movcuddur");

                return View();
            }
            exsisted.Name = vm.Name;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin")]


        //DELETE

        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) throw new WrongRequestException("The query is incorrect");

            Facility existed = await _context.Facilities.FirstOrDefaultAsync(c => c.Id == id);
           
            if (existed == null) return Json(new { status = 404 });
            try
            {


                _context.Facilities.Remove(existed);
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
