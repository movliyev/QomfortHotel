﻿using Microsoft.AspNetCore.Mvc;

namespace Hotel_Managment.MVC_Qomfort_Project.Controllers
{
    public class HomeController : Controller
    {
       
        public IActionResult Index()
        {
            return View();
        }

      
    }
}