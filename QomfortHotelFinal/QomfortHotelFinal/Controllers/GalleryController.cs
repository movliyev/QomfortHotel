using Microsoft.AspNetCore.Mvc;

namespace QomfortHotelFinal.Controllers
{
    public class GalleryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
