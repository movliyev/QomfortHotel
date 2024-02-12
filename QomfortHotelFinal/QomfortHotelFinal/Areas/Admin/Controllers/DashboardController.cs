using Microsoft.AspNetCore.Mvc;

namespace QomfortHotelFinal.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
