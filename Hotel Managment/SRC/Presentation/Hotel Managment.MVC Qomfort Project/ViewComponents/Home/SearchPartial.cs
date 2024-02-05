using Microsoft.AspNetCore.Mvc;

namespace Hotel_Managment.MVC_Qomfort_Project.ViewComponents.Home
{
    public class SearchPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
