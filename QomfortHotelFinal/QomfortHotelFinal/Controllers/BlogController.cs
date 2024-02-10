using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QomfortHotelFinal.DAL;
using QomfortHotelFinal.Models;
using QomfortHotelFinal.ViewModels;

namespace QomfortHotelFinal.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;

        public BlogController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Blog> blog = _context.Blogs.Include(x => x.Comments).ToList();
            return View(blog);
        }
        public async Task<IActionResult> Detail(int id)
        {
            if (id <= 0) return BadRequest();
            Blog blog = await _context.Blogs.Include(x => x.Comments.Where(x => x.BlogId == id)).FirstOrDefaultAsync(b => b.Id == id);
            if (blog == null) return NotFound();

           
            return View(blog);
        }
    }
}
