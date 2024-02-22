using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QomfortHotelFinal.Areas.Admin.ViewModels
{
    public class UpdateSlideVM
    {
        
       
        public string Image { get; set; }
        [MaxLength(100, ErrorMessage = "No more than 100 characters")]
        [MinLength(3, ErrorMessage = "No less than 3 characters")]
        public string Title { get; set; }
        [MaxLength(256, ErrorMessage = "No more than 256 characters")]
        [MinLength(3, ErrorMessage = "No less than 3 characters")]
        public string Description { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
