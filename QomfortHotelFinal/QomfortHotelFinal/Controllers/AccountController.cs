using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QomfortHotelFinal.Abstractions.MailService;
using QomfortHotelFinal.Models;
using QomfortHotelFinal.Utilities.Enums;
using QomfortHotelFinal.Utilities.Exceptions;
using QomfortHotelFinal.Utilities.Extensions;
using QomfortHotelFinal.ViewModels;
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
        private readonly IEmailService _ser;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> role, IWebHostEnvironment env,  IEmailService ser )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _role = role;
            _env = env;
            _ser = ser;
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
           

            AppUser appUser = new AppUser
            {
                
                PhoneNumber = vm.PhoneNumber,   
                Name = vm.Name.Capitalize(),
                Surname = vm.Surname.Capitalize(),
                Gender = vm.Gender,
                Email = vm.Email,
                UserName = vm.UserName.Capitalize(),
            };

            var result = await _userManager.CreateAsync(appUser, vm.Password);
            if (result.Succeeded)
            {

                var token = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);

                var confirmationlink = Url.Action(nameof(ConfirmEmail), "Account", new { token, Email = appUser.Email }, Request.Scheme);

                string buttonHtml = $"<a href='{confirmationlink}' style='background-color: #17a2b8; color: white; text-decoration: none; padding: 10px 20px; border-radius: 5px; display: inline-block;'>Confirm Email</a>";

                await _ser.SendEmailAsync(appUser.Email, "Email Confirmation", buttonHtml);
                await _userManager.AddToRoleAsync(appUser, UserRole.Memmber.ToString());

                return RedirectToAction(nameof(SuccesRegistered), "Account");

            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(String.Empty, item.Description);
                }
            }
           
            return View(vm);
        }
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            AppUser user = await _userManager.FindByEmailAsync(email);
            if (user == null) throw new NotFoundException("User not found");
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                return BadRequest();

            }
            await _signInManager.SignInAsync(user, false);
            return View();
        }



        public async Task<IActionResult> CreateRoles()
        {
            foreach (var role in Enum.GetValues(typeof(UserRole)))
            {
                if(!await _role.RoleExistsAsync(role.ToString()))
                {
                    await _role.CreateAsync(new IdentityRole
                    {
                        Name = role.ToString(),
                    });
                }
               
            }
            return RedirectToAction("Index","Home");   
        }



        public IActionResult SuccesRegistered()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM vm,string? returnUrl)
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
            if (!user.EmailConfirmed)
            {
                ModelState.AddModelError(String.Empty, "Email tesdiq edildi");
                return View();
            }
            if (!result.Succeeded)
            {
                ModelState.AddModelError(String.Empty, "Username , email or Password  incorrect");
                return View(vm);
            }

            //await _signInManager.SignInAsync(user, false);

            //return RedirectToAction("Update", "Profile", new { area ="Memmber"});
            if(returnUrl is null)
            {
                return RedirectToAction("Index", "Home");
            }
            return Redirect(returnUrl);
        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM vm)
        {
            if(!ModelState.IsValid)return View(vm);
            var user=await _userManager.FindByEmailAsync(vm.Email);
            if(user == null) return NotFound();
            //https://localhost:
            string token = await _userManager.GeneratePasswordResetTokenAsync(user);
            string link = Url.Action("ResetPassword", "Account", new { userid = user.Id, token = token }, HttpContext.Request.Scheme);

            string buttonHtml = $"<a href='{link}' style='background-color: #17a2b8; color: white; text-decoration: none; padding: 10px 20px; border-radius: 5px; display: inline-block;'>Reset Password</a>";

            string body = $"Please click the following link to reset your password: {buttonHtml}";
            await _ser.SendEmailAsync(user.Email,"ResetPassword", body, true);
            return RedirectToAction(nameof(Login));
        }

        public async Task<IActionResult>ResetPassword(string userId,string token)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token)) return BadRequest();
            var user=await _userManager.FindByIdAsync(userId);
            if(user == null) return NotFound(); 
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>ResetPassword(ResetPasswordVM vm, string userId, string token)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token)) return BadRequest();
            if (!ModelState.IsValid) return View(vm);
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) throw new NotFoundException("User not found");
            var identityuser = await _userManager.ResetPasswordAsync(user, token, vm.ConfirmPassword);
            return RedirectToAction(nameof(Login));
        }

    }
}
