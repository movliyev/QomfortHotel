using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QomfortHotelFinal.Areas.Admin.ViewModels.Layout;
using QomfortHotelFinal.DAL;
using QomfortHotelFinal.Models;

namespace QomfortHotelFinal.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class LayoutController : Controller
    {
        private readonly UserManager<AppUser> _userman;

        public LayoutController(UserManager<AppUser> userman)
        {
            _userman = userman;
        }
        public async Task<IActionResult> Index()
        {
            AppUser user = await _userman.FindByNameAsync(User.Identity.Name);
            LayoutVM vm = new LayoutVM
            {
                AppUser = user,
            };
            return View(vm);
        }
    }
}
