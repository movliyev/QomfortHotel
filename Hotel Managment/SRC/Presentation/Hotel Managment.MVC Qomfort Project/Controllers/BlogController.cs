using Microsoft.AspNetCore.Mvc;

namespace Hotel_Managment.MVC_Qomfort_Project.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Detail(int id)
        {

            return View();  
        }
    }
}
