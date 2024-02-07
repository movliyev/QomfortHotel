using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Managment.Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }
      
        public string CoomentUser { get; set; }
        public DateTime CommentDate { get; set; }
        public string CommentContent { get; set; }
        public string CommentState { get; set; }
        public int? BlogId { get; set; }
        public Blog? Blog { get; set; }

    }
}
