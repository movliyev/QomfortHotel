using Microsoft.AspNetCore.Mvc;

namespace QomfortHotelFinal.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
