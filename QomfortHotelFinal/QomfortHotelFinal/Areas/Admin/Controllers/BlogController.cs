using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QomfortHotelFinal.Areas.Admin.ViewModels;
using QomfortHotelFinal.Areas.Admin.ViewModels.Blog;
using QomfortHotelFinal.DAL;
using QomfortHotelFinal.Models;
using QomfortHotelFinal.Utilities.Extensions;

namespace QomfortHotelFinal.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]

    public class BlogController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public BlogController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Indexx(int page = 1)
        {
            int count = await _context.Categories.CountAsync();

            List<Blog> Blogs = await _context.Blogs.OrderByDescending(x=>x.Id).Include(x=>x.Comments).Skip((page - 1) * 3).Take(3).ToListAsync();
            PaginateVM<Blog> pagvm = new PaginateVM<Blog>
            {
                Items = Blogs,
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
        public async Task<IActionResult> Create(CreateBlogVM Blogvm)
        {


            //if (Blogvm.Photo is null)
            //{
            //    ModelState.AddModelError("Photo", "Shekil mutleq secilmelidir");
            //    return View();
            //}

            if (!ModelState.IsValid)
            {
                return View(Blogvm);
            }
            if (Blogvm.HoverPhoto is not null)
            {
                if (!Blogvm.HoverPhoto.ValidateType("image/"))
                {
                    ModelState.AddModelError("Photo", "File tipi uyqun deyil");
                    return View();
                }

                if (!Blogvm.HoverPhoto.ValidateSize(2 * 1024))
                {
                    ModelState.AddModelError("Photo", "File olcusu 2-mb den boyuk olmamalidir");
                    return View();

                }

            }
            string hoverp = await Blogvm.HoverPhoto.CreateFileAsync(_env.WebRootPath, "assets", "images", "blog");

            if (Blogvm.MainPhoto is not null)
            {
                if (!Blogvm.MainPhoto.ValidateType("image/"))
                {
                    ModelState.AddModelError("Photo", "File tipi uyqun deyil");
                    return View();
                }

                if (!Blogvm.MainPhoto.ValidateSize(2 * 1024))
                {
                    ModelState.AddModelError("Photo", "File olcusu 2-mb den boyuk olmamalidir");
                    return View();

                }

            }
            string main = await Blogvm.HoverPhoto.CreateFileAsync(_env.WebRootPath, "assets", "images", "blog");


            Blog Blog = new Blog
            {
                HoverImage = hoverp,
                MainImage = main,
                BlogDate = DateTime.Now,
                Title = Blogvm.Title.Capitalize(),
                 Desc1 = Blogvm.Desc1,
                 Desc2 = Blogvm.Desc2,
                 

            };
            await _context.Blogs.AddAsync(Blog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Indexx));
        }


        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) return BadRequest();
            Blog exsist = await _context.Blogs.FirstOrDefaultAsync(s => s.Id == id);
            if (exsist == null) return NotFound();

            UpdateBlogVm Blogvm = new UpdateBlogVm
            {
                HoverImage = exsist.HoverImage,
                MainImage = exsist.MainImage,
                Title = exsist.Title,
                Desc1 = exsist.Desc1,
                Desc2 = exsist.Desc2,
                
            };

            return View(Blogvm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateBlogVm Blogvm)
        {

            if (!ModelState.IsValid)
            {
                return View(Blogvm);
            }

            Blog exsist = await _context.Blogs.FirstOrDefaultAsync(s => s.Id == id);
            if (exsist == null) return NotFound();

            if (Blogvm.HoverPhoto is not null)
            {
                if (!Blogvm.HoverPhoto.ValidateType("image/"))
                {
                    ModelState.AddModelError("Photo", "File tipi uyqun deyil");
                    return View(Blogvm);
                }

                if (!Blogvm.HoverPhoto.ValidateSize(2 * 1024))
                {
                    ModelState.AddModelError("Photo", "File olcusu 2-mb den boyuk olmamalidir");
                    return View(Blogvm);

                }
                string filename = await Blogvm.HoverPhoto.CreateFileAsync(_env.WebRootPath, "assets", "images", "blog");
                exsist.HoverImage.DeleteFile(_env.WebRootPath, "assets", "images", "blog");
                exsist.HoverImage = filename;
            }
            if (Blogvm.MainPhoto is not null)
            {
                if (!Blogvm.MainPhoto.ValidateType("image/"))
                {
                    ModelState.AddModelError("Photo", "File tipi uyqun deyil");
                    return View(Blogvm);
                }

                if (!Blogvm.MainPhoto.ValidateSize(2 * 1024))
                {
                    ModelState.AddModelError("Photo", "File olcusu 2-mb den boyuk olmamalidir");
                    return View(Blogvm);

                }
                string filename = await Blogvm.MainPhoto.CreateFileAsync(_env.WebRootPath, "assets", "images", "blog");
                exsist.MainImage.DeleteFile(_env.WebRootPath, "assets", "images", "blog");
                exsist.MainImage = filename;
            }
            exsist.Title = Blogvm.Title.Capitalize();
            exsist.Desc1 = Blogvm.Desc1;
            exsist.Desc2 = Blogvm.Desc2;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Indexx));
        }




        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();
            Blog exsist = await _context.Blogs.FirstOrDefaultAsync(s => s.Id == id);
            if (exsist == null) return NotFound();
            exsist.MainImage.DeleteFile(_env.WebRootPath, "assets", "images", "blog");
            exsist.HoverImage.DeleteFile(_env.WebRootPath, "assets", "images", "blog");
            _context.Blogs.Remove(exsist);
            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Indexx));
        }


    }
}
