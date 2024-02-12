﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QomfortHotelFinal.Areas.Admin.ViewModels;
using QomfortHotelFinal.DAL;
using QomfortHotelFinal.Models;
using QomfortHotelFinal.Utilities.Extensions;

namespace QomfortHotelFinal.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServiceeController : Controller
    {
        private readonly AppDbContext _context;

        public ServiceeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            if (page < 1) return BadRequest();

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


        //UPDATE 
        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) return BadRequest();
            Servicee Servicee = await _context.Servisees.FirstOrDefaultAsync(c => c.Id == id);
            if (Servicee == null) return NotFound();
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
            if (!ModelState.IsValid)
            {
                return View();
            }
            Servicee exsisted = await _context.Servisees.FirstOrDefaultAsync(c => c.Id == id);
            if (exsisted == null) return NotFound();
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


        //DELETE

        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();

            Servicee existed = await _context.Servisees.FirstOrDefaultAsync(c => c.Id == id);

            if (existed is null) return NotFound();
            _context.Servisees.Remove(existed);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}