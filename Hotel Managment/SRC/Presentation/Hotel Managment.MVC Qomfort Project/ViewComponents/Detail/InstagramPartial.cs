using Microsoft.AspNetCore.Mvc;

namespace Hotel_Managment.MVC_Qomfort_Project.ViewComponents.Detail
{
    public class InstagramPartial:ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
