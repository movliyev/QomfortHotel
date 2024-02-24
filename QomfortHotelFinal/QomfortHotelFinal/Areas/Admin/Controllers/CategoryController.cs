using Microsoft.AspNetCore.Mvc;
using QomfortHotelFinal.Areas.Admin.ViewModels;
using QomfortHotelFinal.DAL;
using QomfortHotelFinal.Models;
using Microsoft.EntityFrameworkCore;
using QomfortHotelFinal.Utilities.Extensions;
using Microsoft.AspNetCore.Authorization;
using QomfortHotelFinal.Utilities.Exceptions;

namespace QomfortHotelFinal.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
   
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Admin,Memmber,Blogger")]
        public async Task<IActionResult> Index(int page = 1)
        {
            if (page < 1) throw new WrongRequestException("The query is incorrect");

            int count = await _context.Categories.CountAsync();
            List<Category> categories = await _context.Categories.Skip((page - 1) * 3).Take(3)
                .Include(c => c.Rooms).ToListAsync();
            if (categories == null) throw new NotFoundException("category not found");
            PaginateVM<Category> pagvm = new PaginateVM<Category>
            {
                Items = categories,
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
        public async Task<IActionResult> Create(CreateCategoryVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View();

            }
            bool result = _context.Categories.Any(c => c.Name.Trim() == vm.Name.Trim());
            if (result)
            {
                ModelState.AddModelError("Name", "Bu adda category movcuddur");
                return View();
            }

            Category category = new Category
            {
                Name = vm.Name.Capitalize(),    
            };
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]

        //UPDATE 
        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) throw new WrongRequestException("The query is incorrect");
            Category category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null) throw new NotFoundException("category not found");
            UpdateCategoryVM vm = new UpdateCategoryVM
            {
                Name = category.Name.Capitalize()   
            };

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateCategoryVM categoryvm)
        {
            if(id<=0) throw new WrongRequestException("The query is incorrect");
            if (!ModelState.IsValid)
            {
                return View();
            }
            Category exsisted = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (exsisted == null) throw new NotFoundException("category not found");
            bool result = await _context.Categories.AnyAsync(c => c.Name == categoryvm.Name.Capitalize() && c.Id != id);
            if (result)
            {
                ModelState.AddModelError("Name", "Bu adda category atriq movcuddur");

                return View();
            }
            exsisted.Name = categoryvm.Name.Capitalize();
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        //DELETE
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) throw new WrongRequestException("The query is incorrect");

            Category existed = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (existed == null) return Json(new { status = 404 });
            try
            {
                _context.Categories.Remove(existed);
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
