using Microsoft.AspNetCore.Mvc;

namespace QomfortHotelFinal.ViewComponents.Detail
{
    public class InstagramPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
