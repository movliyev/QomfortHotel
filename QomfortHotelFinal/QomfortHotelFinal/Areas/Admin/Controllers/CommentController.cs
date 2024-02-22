using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QomfortHotelFinal.Areas.Admin.ViewModels;
using QomfortHotelFinal.Areas.Admin.ViewModels.Blog;
using QomfortHotelFinal.DAL;
using QomfortHotelFinal.Models;

namespace QomfortHotelFinal.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    [Authorize(Roles = "Admin")]
    public class CommentController : Controller
    {
        private readonly AppDbContext _context;

        public CommentController(AppDbContext context)
        {
           _context = context;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            if (page < 1) return BadRequest();
            int count = await _context.Comments.CountAsync();

            List<Comment> comment = await _context.Comments.OrderByDescending(x => x.Id).Include(x => x.Blog)
                .Include(x=>x.AppUser)
                .Skip((page - 1) * 3).Take(3).ToListAsync();
            PaginateVM<Comment> pagvm = new PaginateVM<Comment>
            {
                Items = comment,
                TotalPage = Math.Ceiling((double)count / 3),
                CurrentPage = page,
            };

            return View(pagvm);
        }
       

        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();
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
