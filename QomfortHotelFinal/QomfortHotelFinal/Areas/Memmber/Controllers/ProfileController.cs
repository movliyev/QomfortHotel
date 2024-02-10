using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QomfortHotelFinal.Areas.Memmber.ViewModels;
using QomfortHotelFinal.Models;
using QomfortHotelFinal.Utilities.Extensions;

namespace QomfortHotelFinal.Areas.Memmber.Controllers
{
    [Area("Memmber")]
    [Route("Memmber/[controller]/[action]")]
    public class ProfileController : Controller
    {
        private readonly UserManager<AppUser> _userman;
        private readonly IWebHostEnvironment _env;

        public ProfileController(UserManager<AppUser> userman,IWebHostEnvironment env)
        {
            _userman = userman;
            _env = env;
        }
        public async Task<IActionResult> Update()
        {
            AppUser values = await _userman.FindByNameAsync(User.Identity.Name);
            UserEditVM vm = new UserEditVM();

            vm.name = values.Name;
            vm.surname = values.Surname;
            vm.phonenumber = values.PhoneNumber;
            vm.email = values.Email;
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
                user.UserImage.DeleteFile(_env.WebRootPath, "assets", "images", "User");
                user.UserImage = filename;
            }
            user.Name = vm.name;
            user.Surname = vm.surname;
            user.PhoneNumber = vm.phonenumber;
            user.PasswordHash = _userman.PasswordHasher.HashPassword(user,vm.password);
            var result=await _userman.UpdateAsync(user);    
            if(result.Succeeded)
            {
                return RedirectToAction("Login", "Account",new {are=""});
            }
            return View(vm);
        }
    }
}
