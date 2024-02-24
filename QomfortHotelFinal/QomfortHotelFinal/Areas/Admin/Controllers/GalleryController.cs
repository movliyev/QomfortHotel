using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QomfortHotelFinal.Areas.Admin.ViewModels;
using QomfortHotelFinal.Areas.Admin.ViewModels.Gallery;
using QomfortHotelFinal.DAL;
using QomfortHotelFinal.Models;
using QomfortHotelFinal.Utilities.Exceptions;
using QomfortHotelFinal.Utilities.Extensions;

namespace QomfortHotelFinal.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class GalleryController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public GalleryController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        [Authorize(Roles = "Admin,Memmber")]

        public async Task<IActionResult> Index(int page = 1)
        {
            int count = await _context.Galleries.CountAsync();

            List<Gallery> Gallerys = await _context.Galleries.Skip((page - 1) * 3).Take(3).ToListAsync();

            PaginateVM<Gallery> pagvm = new PaginateVM<Gallery>
            {
                Items = Gallerys,
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
        public async Task<IActionResult> Create(CreateGalleryVM Galleryvm)
        {
                        
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (Galleryvm.Photo is not null)
            {
                if (!Galleryvm.Photo.ValidateType("image/"))
                {
                    ModelState.AddModelError("Photo", "File tipi uyqun deyil");
                    return View();
                }

                if (!Galleryvm.Photo.ValidateSize(2 * 1024))
                {
                    ModelState.AddModelError("Photo", "File olcusu 2-mb den boyuk olmamalidir");
                    return View();

                }

            }
            string filename = await Galleryvm.Photo.CreateFileAsync(_env.WebRootPath, "assets", "images", "gallery");


            Gallery Gallery = new Gallery
            {
                Image = filename,
                Name = Galleryvm.Name.Capitalize(),
               

            };
            await _context.Galleries.AddAsync(Gallery);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) throw new WrongRequestException("The query is incorrect");
            Gallery exsist = await _context.Galleries.FirstOrDefaultAsync(s => s.Id == id);
            if (exsist == null) throw new NotFoundException("Gallery not found");

            UpdateGalleryVM Galleryvm = new UpdateGalleryVM
            {
              
                Image = exsist.Image,
                Name = exsist.Name
            };

            return View(Galleryvm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateGalleryVM Galleryvm)
        {
            if (id <= 0) throw new WrongRequestException("The query is incorrect");

            if (!ModelState.IsValid)
            {
                return View(Galleryvm);
            }

            Gallery exsist = await _context.Galleries.FirstOrDefaultAsync(s => s.Id == id);
            if (exsist == null) return NotFound();

            if (Galleryvm.Photo is not null)
            {
                if (!Galleryvm.Photo.ValidateType("image/"))
                {
                    ModelState.AddModelError("Photo", "File tipi uyqun deyil");
                    return View(Galleryvm);
                }

                if (!Galleryvm.Photo.ValidateSize(2 * 1024))
                {
                    ModelState.AddModelError("Photo", "File olcusu 2-mb den boyuk olmamalidir");
                    return View(Galleryvm);

                }
                string filename = await Galleryvm.Photo.CreateFileAsync(_env.WebRootPath, "assets", "images", "gallery");
                exsist.Image.DeleteFile(_env.WebRootPath, "assets", "images", "gallery");
                exsist.Image = filename;
            }
            exsist.Name = Galleryvm.Name.Capitalize();
          
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        [Authorize(Roles = "Admin")]


        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) throw new WrongRequestException("The query is incorrect");
            Gallery exsist = await _context.Galleries.FirstOrDefaultAsync(s => s.Id == id);
            if (exsist == null) return Json(new { status = 404 });
            try
            {


                exsist.Image.DeleteFile(_env.WebRootPath, "assets", "image", "gallery");
                _context.Galleries.Remove(exsist);
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
