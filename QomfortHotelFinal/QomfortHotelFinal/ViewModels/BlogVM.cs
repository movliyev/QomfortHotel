using QomfortHotelFinal.Models;
using System.ComponentModel.DataAnnotations;

namespace QomfortHotelFinal.ViewModels
{
    public class BlogVM
    {
        public Blog? Blog { get; set; }
        public List<Comment>? Comments { get; set; }
        public DateTime CommentDate { get; set; }
        [Required(ErrorMessage = "A CommentContent must be included")]
        [MaxLength(256, ErrorMessage = "No more than 256 characters")]
        [MinLength(4, ErrorMessage = "Be less than 4 characters")]
        public string CommentContent { get; set; }
        public bool CommentStatus { get; set; }
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
        public int? Rating { get; set; }
    }
}
