using Microsoft.AspNetCore.Mvc;
using QomfortHotelFinal.DAL;
using QomfortHotelFinal.Models;

namespace QomfortHotelFinal.Controllers
{
    public class CommentController : Controller
    {
        private readonly AppDbContext _context;

        public CommentController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public PartialViewResult AddComment()
        {
            return PartialView();
        }
        [HttpPost]
        public IActionResult AddComment(Comment p)
        {
            if(!ModelState.IsValid)
            {
                return View(p);
            }
            p.CommentDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            p.CommentState = true;
            p.BlogId = 1;
            _context.Comments.Add(p);
            _context.SaveChanges();
            return RedirectToAction("Index", "Blog");
        }
    }
}
