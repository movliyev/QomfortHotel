﻿using QomfortHotelFinal.Models;
using System.ComponentModel.DataAnnotations;

namespace QomfortHotelFinal.ViewModels
{
    public class HomeVM
    {
        public List<Room> Rooms { get; set; }
        public List<Slide> Slides { get; set; }
        public List<Servicee> Servicees { get; set; }
        public HomeAbout HomeAbouts { get; set; }
        public List<Gallery> Galleries { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Blog> Blogs { get; set; }


       
    }
}
