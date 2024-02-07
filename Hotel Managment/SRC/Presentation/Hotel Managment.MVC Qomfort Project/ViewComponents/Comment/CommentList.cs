using Hotel_Managment.Rersistance.Implementations.Service;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Managment.MVC_Qomfort_Project.ViewComponents.Comment
{
    public class CommentList : ViewComponent
    {
        private readonly CommentService _ser;

        public CommentList(CommentService ser)
        {
            _ser = ser;
        }
        public IViewComponentResult Invoke(int id)
        {
            var values=_ser.TGetByBlogId(id);
            return View(values);

        }
    }
}
