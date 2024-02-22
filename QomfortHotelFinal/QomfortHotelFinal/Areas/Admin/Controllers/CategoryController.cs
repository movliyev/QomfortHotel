﻿using Microsoft.AspNetCore.Mvc;
using QomfortHotelFinal.Areas.Admin.ViewModels;
using QomfortHotelFinal.DAL;
using QomfortHotelFinal.Models;
using Microsoft.EntityFrameworkCore;
using QomfortHotelFinal.Utilities.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace QomfortHotelFinal.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    [Authorize(Roles = "Admin,Memmber")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            if (page < 1) return BadRequest();

            int count = await _context.Categories.CountAsync();
            List<Category> categories = await _context.Categories.Skip((page - 1) * 3).Take(3)
                .Include(c => c.Rooms).ToListAsync();
            PaginateVM<Category> pagvm = new PaginateVM<Category>
            {
                Items = categories,
                TotalPage = Math.Ceiling((double)count / 3),
                CurrentPage = page,
            };

            return View(pagvm);
        }
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


        //UPDATE 
        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) return BadRequest();
            Category category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null) return NotFound();
            UpdateCategoryVM vm = new UpdateCategoryVM
            {
                Name = category.Name.Capitalize()   
            };

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateCategoryVM categoryvm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Category exsisted = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (exsisted == null) return NotFound();
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

        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();

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
