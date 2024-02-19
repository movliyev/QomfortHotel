using QomfortHotelFinal.Models;
using System.ComponentModel.DataAnnotations;

namespace QomfortHotelFinal.ViewModels
{
    public class BlogVM
    {
        public Blog? Blog { get; set; }
        public List<Comment>? Comments { get; set; }
        public DateTime CommentDate { get; set; }
        public string CommentContent { get; set; }
        public bool CommentStatus { get; set; }
    }
}
