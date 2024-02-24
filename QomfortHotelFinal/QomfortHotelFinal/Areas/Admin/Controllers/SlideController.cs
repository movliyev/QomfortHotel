using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QomfortHotelFinal.Areas.Admin.ViewModels;
using QomfortHotelFinal.DAL;
using QomfortHotelFinal.Models;
using QomfortHotelFinal.Utilities.Exceptions;
using QomfortHotelFinal.Utilities.Extensions;

namespace QomfortHotelFinal.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class SlideController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SlideController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        [Authorize(Roles = "Admin,Memmber,Blogger")]

        public async Task<IActionResult> Index(int page = 1)
        {
            if(page<1) throw new WrongRequestException("The query is incorrect");
            int count = await _context.Categories.CountAsync();

            List<Slide> slides = await _context.Slides.Skip((page - 1) * 3).Take(3).ToListAsync();
            PaginateVM<Slide> pagvm = new PaginateVM<Slide>
            {
                Items = slides,
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
        public async Task<IActionResult> Create(CreateSlideVM slidevm)
        {

           

            if (!ModelState.IsValid)
            {
                return View();
            }
            if(slidevm.Photo is not null)
            {
                if (!slidevm.Photo.ValidateType("image/"))
                {
                    ModelState.AddModelError("Photo", "File tipi uyqun deyil");
                    return View();
                }

                if (!slidevm.Photo.ValidateSize(2 * 1024))
                {
                    ModelState.AddModelError("Photo", "File olcusu 2-mb den boyuk olmamalidir");
                    return View();

                }

            }
                string filename = await slidevm.Photo.CreateFileAsync(_env.WebRootPath, "assets", "images", "slider");
          

            Slide slide = new Slide
            {
                Image = filename,
                Title = slidevm.Title.Capitalize(),
                Description = slidevm.Description,

            };
            await _context.Slides.AddAsync(slide);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin")]


        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) throw new WrongRequestException("The query is incorrect");

            Slide exsist = await _context.Slides.FirstOrDefaultAsync(s => s.Id == id);
            if (exsist == null) throw new NotFoundException("Slide not found");

            UpdateSlideVM slidevm = new UpdateSlideVM
            {
                Description = exsist.Description,
                Image = exsist.Image,
                Title = exsist.Title
            };

            return View(slidevm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateSlideVM slidevm)
        {
            if (id <= 0) throw new WrongRequestException("The query is incorrect");

            if (!ModelState.IsValid)
            {
                return View(slidevm);
            }

            Slide exsist = await _context.Slides.FirstOrDefaultAsync(s => s.Id == id);
            if (exsist == null) throw new NotFoundException("Slide not found");

            if (slidevm.Photo is not null)
            {
                if (!slidevm.Photo.ValidateType("image/"))
                {
                    ModelState.AddModelError("Photo", "File tipi uyqun deyil");
                    return View(slidevm);
                }

                if (!slidevm.Photo.ValidateSize(2 * 1024))
                {
                    ModelState.AddModelError("Photo", "File olcusu 2-mb den boyuk olmamalidir");
                    return View(slidevm);

                }
                string filename = await slidevm.Photo.CreateFileAsync(_env.WebRootPath, "assets", "images", "slider");
                exsist.Image.DeleteFile(_env.WebRootPath, "assets", "images", "slider");
                exsist.Image = filename;
            }
            exsist.Title = slidevm.Title.Capitalize();
            exsist.Description = slidevm.Description;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        [Authorize(Roles = "Admin")]




        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();
            Slide exsist = await _context.Slides.FirstOrDefaultAsync(s => s.Id == id);
            if (exsist == null) return Json(new { status = 404 });
            try
            {


                exsist.Image.DeleteFile(_env.WebRootPath, "assets", "image", "slider");
                _context.Slides.Remove(exsist);
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
