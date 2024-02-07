﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Managment.Domain.Entities
{
    public class HomeAbout
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Img1 { get; set; }
        public string Img2 { get; set;}
        public string Img3 { get; set;}
        public List<Servicee>? Servicees { get; set; }
        public List<Room>? Rooms { get; set; }
        public string rImg { get; set; }

    }
}