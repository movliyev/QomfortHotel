using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QomfortHotelFinal.Areas.Admin.ViewModels;
using QomfortHotelFinal.Areas.Admin.ViewModels.Blog;
using QomfortHotelFinal.DAL;
using QomfortHotelFinal.Models;
using QomfortHotelFinal.Utilities.Exceptions;

namespace QomfortHotelFinal.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    [Authorize(Roles = "Admin,Blogger")]
    public class CommentController : Controller
    {
        private readonly AppDbContext _context;

        public CommentController(AppDbContext context)
        {
           _context = context;
        }
        
        public async Task<IActionResult> Index(int page = 1)
        {
            if (page < 1) throw new WrongRequestException("The query is incorrect");
            int count = await _context.Comments.CountAsync();

            List<Comment> comment = await _context.Comments.OrderByDescending(x => x.Id).Include(x => x.Blog)
                .Include(x=>x.AppUser)
                .Skip((page - 1) * 3).Take(3).ToListAsync();
            if (comment == null) throw new NotFoundException("comment not found");

            PaginateVM<Comment> pagvm = new PaginateVM<Comment>
            {
                Items = comment,
                TotalPage = Math.Ceiling((double)count / 3),
                CurrentPage = page,
            };

            return View(pagvm);
        }
        public async Task<IActionResult> UpdateStatus(int id)
        {
            if(id<=0) throw new WrongRequestException("The query is incorrect");
            if (!ModelState.IsValid)
            {
                return View();
            }
            Comment exsisted = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
            if (exsisted == null) throw new NotFoundException("comment not found");
            return View(exsisted);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int id, bool? status)
        {
            if (id <= 0) throw new WrongRequestException("The query is incorrect");

            if (!ModelState.IsValid)
            {
                return View();
            }
            Comment comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
            if (comment == null) throw new NotFoundException("comment not found");


            comment.CommentStatus = status ?? true;


            await _context.SaveChangesAsync();
           
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) throw new WrongRequestException("The query is incorrect");
            Comment exsist = await _context.Comments.FirstOrDefaultAsync(s => s.Id == id);
            if (exsist == null) return Json(new { status = 404 });
            try
            {
                
                _context.Comments.Remove(exsist);
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
