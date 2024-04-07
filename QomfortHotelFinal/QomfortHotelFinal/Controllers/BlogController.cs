

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QomfortHotelFinal.DAL;
using QomfortHotelFinal.Models;
using QomfortHotelFinal.Utilities.Exceptions;
using QomfortHotelFinal.ViewModels;
using System.Security.Claims;

namespace QomfortHotelFinal.Controllers
{
    [AllowAnonymous]

    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public BlogController(AppDbContext context,UserManager<AppUser> userManager)
        {
            _context = context;
           _userManager = userManager;
        }
        public async Task <IActionResult> Index(int page = 1)
        {
            if (page < 1) return BadRequest();

            int count = await _context.Blogs.CountAsync();

            List<Blog> blog =await _context.Blogs.Include(x => x.Comments).ToListAsync();
            PaginateVM<Blog> pagvm = new PaginateVM<Blog>
            {
                Items = blog,
                TotalPage = Math.Ceiling((double)count / 3),
                CurrentPage = page,
            };

            return View(pagvm);
        }
        public async Task<IActionResult> Detail(int id)
        {
            if (id == 0) throw new WrongRequestException("The query is incorrect");
            AppUser user = await _userManager.Users
           .Include(u => u.Comments)
        .ThenInclude(bi => bi.Blog)
        .FirstOrDefaultAsync(u => u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));
                    

            Blog blog = await _context.Blogs.OrderByDescending(p => p.Id)
                .Include(x => x.Comments.Where(x => x.BlogId == id)).FirstOrDefaultAsync(b => b.Id == id);

            if (blog == null) throw new NotFoundException("Room not found");
            List<Comment> comments = await _context.Comments.Include(x => x.Blog).Include(x=>x.AppUser).OrderByDescending(x=>x.Id).Take(5).Where(x=>x.BlogId==id)
               
                .ToListAsync();
          
            BlogVM blogvm = new BlogVM
            {
               Comments=comments,
               Blog=blog,
            };

            return View(blogvm);
        }

        [HttpPost]
        public async Task<IActionResult> Detail(int id, BlogVM vm)
        {
            if (id <= 0) throw new WrongRequestException("The query is incorrect");
            Blog blog = await _context.Blogs.OrderByDescending(p => p.Id)
               .Include(x => x.Comments.Where(x => x.BlogId == id)).FirstOrDefaultAsync(b => b.Id == id);
            if (blog == null)return NotFound(); 
          
            if (!ModelState.IsValid) throw new NotFoundException("Room not found");



            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);

                Comment comment = new Comment
                {
                   CommentContent=vm.CommentContent,
                   CommentStatus=false,
                   CommentDate=DateTime.Now,
                    BlogId = blog.Id,
                    AppUserId = user.Id,
                    Rate=vm.Rating
                };
               
                await _context.Comments.AddAsync(comment);
                await _context.SaveChangesAsync();
              
            }
            else
            {

                return RedirectToAction("Login", "Account");
            }

            return RedirectToAction("Detail");
        }
    }
}
