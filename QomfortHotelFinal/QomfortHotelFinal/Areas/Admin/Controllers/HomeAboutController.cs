using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    [Authorize(Roles = "Admin")]
    public class HomeAboutController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public HomeAboutController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
           
            List<HomeAbout> HomeAbouts = await _context.HomeAbouts.ToListAsync();
           
            return View(HomeAbouts);
        }
       

        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) throw new WrongRequestException("The query is incorrect");
            HomeAbout exsist = await _context.HomeAbouts.FirstOrDefaultAsync(s => s.Id == id);
            if (exsist == null) return NotFound();

            UpdateHomeAboutVM HomeAboutvm = new UpdateHomeAboutVM
            {
                Description = exsist.Description,
                Img1 = exsist.Img1,
                Img2 = exsist.Img2,
                Img3 = exsist.Img3,
                rImg = exsist.Img3,
                Title = exsist.Title
            };

            return View(HomeAboutvm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateHomeAboutVM HomeAboutvm)
        {

            if (!ModelState.IsValid)
            {
                return View(HomeAboutvm);
            }

            HomeAbout exsist = await _context.HomeAbouts.FirstOrDefaultAsync(s => s.Id == id);
            if (exsist == null) return NotFound();

            if (HomeAboutvm.Photo1 is not null)
            {
                if (!HomeAboutvm.Photo1.ValidateType("image/"))
                {
                    ModelState.AddModelError("Photo", "File tipi uyqun deyil");
                    return View(HomeAboutvm);
                }

                if (!HomeAboutvm.Photo1.ValidateSize(2 * 1024))
                {
                    ModelState.AddModelError("Photo", "File olcusu 2-mb den boyuk olmamalidir");
                    return View(HomeAboutvm);

                }
                string filename = await HomeAboutvm.Photo1.CreateFileAsync(_env.WebRootPath, "assets", "images", "about");
                exsist.Img1.DeleteFile(_env.WebRootPath, "assets", "images", "HomeAboutr");
                exsist.Img1 = filename;
            }
           

            if (HomeAboutvm.Photo2 is not null)
            {
                if (!HomeAboutvm.Photo2.ValidateType("image/"))
                {
                    ModelState.AddModelError("Photo", "File tipi uyqun deyil");
                    return View(HomeAboutvm);
                }

                if (!HomeAboutvm.Photo2.ValidateSize(2 * 1024))
                {
                    ModelState.AddModelError("Photo", "File olcusu 2-mb den boyuk olmamalidir");
                    return View(HomeAboutvm);

                }
                string filename = await HomeAboutvm.Photo2.CreateFileAsync(_env.WebRootPath, "assets", "images", "about");
                exsist.Img2.DeleteFile(_env.WebRootPath, "assets", "images", "HomeAboutr");
                exsist.Img2 = filename;
            }
            if (HomeAboutvm.Photo3 is not null)
            {
                if (!HomeAboutvm.Photo3.ValidateType("image/"))
                {
                    ModelState.AddModelError("Photo", "File tipi uyqun deyil");
                    return View(HomeAboutvm);
                }

                if (!HomeAboutvm.Photo1.ValidateSize(2 * 1024))
                {
                    ModelState.AddModelError("Photo", "File olcusu 2-mb den boyuk olmamalidir");
                    return View(HomeAboutvm);

                }
                string filename = await HomeAboutvm.Photo3.CreateFileAsync(_env.WebRootPath, "assets", "images", "about");
                exsist.Img3.DeleteFile(_env.WebRootPath, "assets", "images", "HomeAboutr");
                exsist.Img3 = filename;
            }
            if (HomeAboutvm.Photor is not null)
            {
                if (!HomeAboutvm.Photor.ValidateType("image/"))
                {
                    ModelState.AddModelError("Photo", "File tipi uyqun deyil");
                    return View(HomeAboutvm);
                }

                if (!HomeAboutvm.Photor.ValidateSize(2 * 1024))
                {
                    ModelState.AddModelError("Photo", "File olcusu 2-mb den boyuk olmamalidir");
                    return View(HomeAboutvm);

                }
                string filename = await HomeAboutvm.Photor.CreateFileAsync(_env.WebRootPath, "assets", "images", "rooms");
                exsist.rImg.DeleteFile(_env.WebRootPath, "assets", "images", "HomeAboutr");
                exsist.rImg = filename;
            }
            exsist.Title = HomeAboutvm.Title.Capitalize();
            exsist.Description = HomeAboutvm.Description;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
       
    }
}
