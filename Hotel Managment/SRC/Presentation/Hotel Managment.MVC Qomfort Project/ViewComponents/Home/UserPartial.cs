using Hotel_Managment.Domain.Entities;
using Hotel_Managment.Rersistance.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Managment.MVC_Qomfort_Project.ViewComponents.Home
{
    public class UserPartial:ViewComponent
    {
        private readonly AppDbContext _context;

        public UserPartial(AppDbContext context)
        {

            _context = context;
        }
         public IViewComponentResult Invoke()
        {

            return View();
        }
    }
}
