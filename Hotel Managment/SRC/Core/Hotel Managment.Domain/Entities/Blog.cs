using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Managment.Domain.Entities
{
    public class Blog
    {
        public int Id { get; set; }
        public DateTime BlogDate { get; set; }
        public string Desc1 { get; set; }
        public string Desc2 { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string HoverImage { get; set; }
        public string MainImage { get; set; }
        public List<Comment>? Comments { get; set; }

    }
}
