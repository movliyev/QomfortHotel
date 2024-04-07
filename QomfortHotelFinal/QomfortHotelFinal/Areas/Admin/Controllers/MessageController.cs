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

    [Authorize(Roles ="Admin")]
    public class MessageController : Controller
    {
        private readonly AppDbContext _context;

        public MessageController(AppDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Index(int page = 1)
        {
            if (page < 1) throw new WrongRequestException("The query is incorrect");
            int count = await _context.Messages.CountAsync();

            List<Message> Message = await _context.Messages.OrderByDescending(x => x.Id)
                .Include(x => x.AppUser)
                .Skip((page - 1) * 3).Take(3).ToListAsync();
            PaginateVM<Message> pagvm = new PaginateVM<Message>
            {
                Items = Message,
                TotalPage = Math.Ceiling((double)count / 3),
                CurrentPage = page,
            };

            return View(pagvm);
        }
        public async Task<IActionResult> UpdateStatus(int id)
        {
            if (id<=0) throw new WrongRequestException("The query is incorrect");

            if (!ModelState.IsValid)
            {
                return View();
            }
            Message exsisted = await _context.Messages.FirstOrDefaultAsync(c => c.Id == id);
            if (exsisted == null) return NotFound();
            return View(exsisted);  
        }
        [HttpPost]
        public async Task<IActionResult>UpdateStatus(int id,bool? status)
        {
            if (id <= 0) throw new WrongRequestException("The query is incorrect");

            if (!ModelState.IsValid)
            {
                return View();
            }
            Message exsisted = await _context.Messages.FirstOrDefaultAsync(c => c.Id == id);
            if (exsisted == null) throw new NotFoundException("message not found");


            exsisted.Status = status.Value;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) throw new WrongRequestException("The query is incorrect");
            Message exsist = await _context.Messages.FirstOrDefaultAsync(s => s.Id == id);
            if (exsist == null) return Json(new { status = 404 });
            try
            {

                _context.Messages.Remove(exsist);
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
