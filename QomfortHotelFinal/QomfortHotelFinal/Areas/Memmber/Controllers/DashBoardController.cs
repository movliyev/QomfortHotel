using Microsoft.AspNetCore.Mvc;

namespace QomfortHotelFinal.Areas.Memmber.Controllers
{
    [Area("Memmber")]
    public class DashBoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
