using System.ComponentModel.DataAnnotations;

namespace QomfortHotelFinal.Areas.Admin.ViewModels.Blog
{
    public class UpdateBlogVm
    {
      
        [MaxLength(256, ErrorMessage = "No more than 256 characters")]
        [MinLength(4, ErrorMessage = "Be less than 4 characters")]
        public string Desc1 { get; set; }
      
        [MaxLength(256, ErrorMessage = "No more than 256 characters")]
        [MinLength(4, ErrorMessage = "Be less than 4 characters")]
        public string Desc2 { get; set; }
      
        [MaxLength(100, ErrorMessage = "No more than 100 characters")]
        [MinLength(4, ErrorMessage = "Be less than 4 characters")]
        public string Title { get; set; }
        public IFormFile HoverPhoto { get; set; }
        public IFormFile MainPhoto { get; set; }
        public string HoverImage { get; set; }
        public string MainImage { get; set; }
    }
}
