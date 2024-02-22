using QomfortHotelFinal.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QomfortHotelFinal.Areas.Admin.ViewModels.Blog
{
    public class CreateBlogVM
    {
        public DateTime BlogDate { get; set; }
        [Required(ErrorMessage = "A description must be included")]
        [MaxLength(256,ErrorMessage = "No more than 256 characters")]
        [MinLength(4,ErrorMessage = "Be less than 4 characters")]
        public string Desc1 { get; set; }
        [Required(ErrorMessage = "A description must be included")]
        [MaxLength(256, ErrorMessage = "No more than 256 characters")]
        [MinLength(4, ErrorMessage = "Be less than 4 characters")]
        public string Desc2 { get; set; }
        [Required(ErrorMessage = "A description must be included")]
        [MaxLength(100, ErrorMessage = "No more than 100 characters")]
        [MinLength(4, ErrorMessage = "Be less than 4 characters")]
        public string Title { get; set; }
        [Required(ErrorMessage = "A HoverPhoto must be included")]
        [NotMapped]
        public IFormFile? HoverPhoto { get; set; }
      
        [Required(ErrorMessage = "A MainPhoto must be included")]
        [NotMapped]
        public IFormFile? MainPhoto { get; set; }
     
    }
}
