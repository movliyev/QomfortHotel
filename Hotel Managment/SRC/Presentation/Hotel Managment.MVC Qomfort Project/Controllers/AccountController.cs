using Hotel_Managment.Domain.Entities;
using Hotel_Managment.MVC_Qomfort_Project.Utilities.Extensions;
using Hotel_Managment.MVC_Qomfort_Project.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Hotel_Managment.MVC_Qomfort_Project.Controllers
{
    //[AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _role;
        private readonly IWebHostEnvironment _env;

        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,RoleManager<AppRole> role,IWebHostEnvironment env)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _role = role;
            _env = env;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task <IActionResult> Register(RegisterVM vm)
        {

            if (!ModelState.IsValid) return View(vm);
            if (vm.Name.isDigit())
            {
                ModelState.AddModelError("Name", "adda reqem olmaz");
                return View(vm);
            }
            if (vm.Surname.isDigit())
            {
                ModelState.AddModelError("Surname", "soyadda reqem olmaz");
                return View(vm);
            }
            if (!vm.Email.CheckEmail())
            {
                ModelState.AddModelError("Email", "Email uslubu yanlisdir");
                return View(vm);
            }
            if (!vm.Photo.ValidateType("image/"))
            {
                ModelState.AddModelError("Photo", "File tipi uyqun deyil");
                return View();
            }

            if (!vm.Photo.ValidateSize(2 * 1024))
            {
                ModelState.AddModelError("Photo", "File olcusu 2-mb den boyuk olmamalidir");
                return View();

            }

            string filename = await vm.Photo.CreateFileAsync(_env.WebRootPath, "assets", "images", "User");

            AppUser appUser = new AppUser
            {
                UserImage=filename,
                Name = vm.Name,
                Surname = vm.Surname,
                Gender = vm.Gender,
                Email = vm.Email,
                UserName = vm.UserName
            };
            
            var result = await _userManager.CreateAsync(appUser, vm.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(String.Empty, item.Description);
                }
                return View(vm);
            }
            await _signInManager.SignInAsync(appUser, false);
            return RedirectToAction("Index" ,"Home");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
    }
}
