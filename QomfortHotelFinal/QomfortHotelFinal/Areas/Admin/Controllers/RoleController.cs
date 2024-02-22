using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using QomfortHotelFinal.Areas.Admin.ViewModels.Role;
using QomfortHotelFinal.Models;
using QomfortHotelFinal.Utilities.Extensions;

namespace QomfortHotelFinal.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]

    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _role;
        private readonly UserManager<AppUser> _userManager;

        public RoleController(RoleManager<IdentityRole> role,UserManager<AppUser> userManager)
        {
            _role = role;
           _userManager = userManager;
        }
        public IActionResult Index()
        {
            var values = _role.Roles.ToList();
            if (values != null)
            {
                return View(values);
            }
            else
            {

                return NotFound();
            }

        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateRoleVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View();

            }
            bool result = _role.Roles.Any(c => c.Name.Trim() == vm.Name.Trim());
            if (result)
            {
                ModelState.AddModelError("Name", "Bu adda Role movcuddur");
                return View();
            }

            IdentityRole Role = new IdentityRole
            {
                Name = vm.Name.Capitalize(),
            };
            var result2 = await _role.CreateAsync(Role);
            return RedirectToAction(nameof(Index));
        }
        
        public IActionResult UserList()
        {
            var values=_userManager.Users.ToList();
            return View(values);
        }

        public async Task<IActionResult> AssignRole(int id)
        {
            var user =  _userManager.Users.FirstOrDefault(x => string.Equals(x.Id, id));
            var roles= _role.Roles.ToList();
            if (user != null)
            {
                // Kullanıcı bulundu, rolleri alabilirsiniz
                var userroles = await _userManager.GetRolesAsync(user);
                // Devam eden işlemler
                List<RoleAssignVM> assign = new List<RoleAssignVM>();
                foreach (var item in roles)
                {
                    RoleAssignVM model = new RoleAssignVM();
                    model.RoleId = int.TryParse(item.Id, out int roleId) ? roleId : 0; model.RoleName = item.Name;
                    model.RoleExsist = userroles.Contains(item.Name);
                    assign.Add(model);

                }
                return View(assign);
            }
           
           return RedirectToAction(nameof(UserList));
           
           
        }
    }
}

