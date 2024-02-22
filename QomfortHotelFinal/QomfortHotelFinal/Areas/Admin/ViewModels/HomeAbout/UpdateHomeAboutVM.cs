using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QomfortHotelFinal.Areas.Admin.ViewModels
{
    public class UpdateHomeAboutVM
    {
        [MaxLength(100, ErrorMessage = "No more than 100 characters")]
        [MinLength(3,ErrorMessage ="No less than 3 characters")]
        public string Title { get; set; }
        [MaxLength(256, ErrorMessage = "No more than 256 characters")]
        [MinLength(3, ErrorMessage = "No less than 3 characters")]
        public string Description { get; set; }
        [NotMapped]
        public IFormFile? Photo1 { get; set; }
        [NotMapped]
        public IFormFile? Photo2 { get; set; }
        [NotMapped]
        public IFormFile? Photo3 { get; set; }
        [NotMapped]
        public IFormFile? Photor { get; set; }
        public string Img1 { get; set; }
        public string Img2 { get; set; }
        public string Img3 { get; set; }
        public string rImg { get; set; }
    }
}
