using Hotel_Managment.Domain.Entities;
using Hotel_Managment.Rersistance.Implementations.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Managment.MVC_Qomfort_Project.Controllers
{
    public class CommentController : Controller
    {
        private readonly CommentService _ser;

        public CommentController(CommentService ser)
        {
            _ser = ser;
        }
        public PartialViewResult AddComment()
        {
            return PartialView();
        }
        [HttpPost]
        public IActionResult AddComment(Comment p)
        {

            p.CommentDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            p.CommentState = true;
            p.BlogId = 1;
            _ser.TAdd(p);
          
         
            return RedirectToAction("Index","Blog");
        }
    }
}
