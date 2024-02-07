using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Managment.Domain.Entities
{
    public class Testimonial
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public string Image { get; set; }
        public bool? Status { get; set; }
    }
}

