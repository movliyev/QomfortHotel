using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QomfortHotelFinal.Models;
using QomfortHotelFinal.Utilities.Extensions;
using QomfortHotelFinal.ViewModels.Account;

namespace QomfortHotelFinal.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _role;
        private readonly IWebHostEnvironment _env;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> role, IWebHostEnvironment env)
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
        public async Task<IActionResult> Register(RegisterVM vm)
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

            //if (!vm.Photo.ValidateType("image/"))
            //{
            //    ModelState.AddModelError("Photo", "File tipi uyqun deyil");
            //    return View();
            //}

            //if (!vm.Photo.ValidateSize(2 * 1024))
            //{
            //    ModelState.AddModelError("Photo", "File olcusu 2-mb den boyuk olmamalidir");
            //    return View();

            //}

            //string filename = await vm.Photo.CreateFileAsync(_env.WebRootPath, "assets", "images", "User");

            AppUser appUser = new AppUser
            {
                //UserImage = filename,
                Name = vm.Name.Capitalize(),
                Surname = vm.Surname.Capitalize(),
                Gender = vm.Gender,
                Email = vm.Email,
                UserName = vm.UserName
            };

            var result = await _userManager.CreateAsync(appUser, vm.Password);
            if (result.Succeeded)
            {

                return RedirectToAction("Index","Home");

            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(String.Empty, item.Description);
                }
            }
            //await _signInManager.SignInAsync(appUser, false);
            return View(vm);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM vm)
        {


            if (!ModelState.IsValid) return View(vm);
            AppUser user = await _userManager.FindByNameAsync(vm.UserNameOrEmail);
            if (user is null)
            {
                user = await _userManager.FindByEmailAsync(vm.UserNameOrEmail);
                if (user is null)
                {
                    ModelState.AddModelError(String.Empty, "Username , email or password  incorrect");
                    return View(vm);
                }
            }
            var result = await _signInManager.PasswordSignInAsync(user, vm.Password, vm.IsRemembered, true);
            if (result.IsLockedOut)
            {
                ModelState.AddModelError(String.Empty, "Account is locked. Please try again after a few minutes.");
                return View(vm);
            }
            if (!result.Succeeded)
            {
                ModelState.AddModelError(String.Empty, "Username , email or Password  incorrect");
                return View(vm);
            }

            //await _signInManager.SignInAsync(user, false);

            return RedirectToAction("Update", "Profile", new { area ="Memmber"});
        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
