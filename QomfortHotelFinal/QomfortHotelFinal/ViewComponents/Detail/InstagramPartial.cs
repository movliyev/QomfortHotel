using Microsoft.AspNetCore.Mvc;

namespace QomfortHotelFinal.ViewComponents.Detail
{
    public class InstagramPartial:ViewComponent
    {
        public async Task <IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
