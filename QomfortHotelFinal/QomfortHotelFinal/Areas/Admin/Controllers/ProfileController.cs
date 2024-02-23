using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QomfortHotelFinal.Areas.Admin.ViewModels;
using QomfortHotelFinal.DAL;
using QomfortHotelFinal.Models;
using QomfortHotelFinal.Utilities.Extensions;

namespace QomfortHotelFinal.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    [Authorize(Roles = "Admin,Memmber,Blogger")]
    public class ProfileController : Controller
    {
        private readonly UserManager<AppUser> _userman;
        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _context;

        public ProfileController(UserManager<AppUser> userman,IWebHostEnvironment env,AppDbContext context)
        {
            _userman = userman;
            _env = env;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            AppUser values = await _userman.FindByNameAsync(User.Identity.Name);
            var user = await _context.Users.FirstOrDefaultAsync(x=>x.Id==values.Id);
            return View(user);
        }
        public async Task<IActionResult> Update()
        {
            AppUser values = await _userman.FindByNameAsync(User.Identity.Name);
            UserEditVM vm = new UserEditVM();

            vm.name = values.Name;
            vm.surname = values.Surname;
            vm.phonenumber = values.PhoneNumber;
           
            vm.image = values.UserImage;
            
            return View(vm);
        }
        [HttpPost]
        public async Task <IActionResult> Update(UserEditVM vm)
        {
            var user = await _userman.FindByNameAsync(User.Identity.Name);
            if (vm.Photo is not null)
            {
                if (!vm.Photo.ValidateType("image/"))
                {
                    ModelState.AddModelError("Photo", "File tipi uyqun deyil");
                    return View(vm);
                }

                if (!vm.Photo.ValidateSize(2 * 1024))
                {
                    ModelState.AddModelError("Photo", "File olcusu 2-mb den boyuk olmamalidir");
                    return View(vm);

                }
                string filename = await vm.Photo.CreateFileAsync(_env.WebRootPath, "assets", "images", "User");
                user.UserImage?.DeleteFile(_env.WebRootPath, "assets", "images", "User");
                user.UserImage = filename;
            }
            user.Name = vm.name;
            user.Surname = vm.surname;
            user.PhoneNumber = vm.phonenumber;
           
            var result=await _userman.UpdateAsync(user);    
            if(result.Succeeded)
            {
                return RedirectToAction("Index" );
            }
            return View(vm);
        }
    }
}
