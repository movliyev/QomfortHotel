using System.ComponentModel.DataAnnotations;

namespace QomfortHotelFinal.Areas.Admin.ViewModels.Gallery
{
    public class UpdateGalleryVM
    {
        [MaxLength(100, ErrorMessage = "No more than 100 characters")]
        [MinLength(4, ErrorMessage = "Be less than 4 characters")]
        public string Name { get; set; }
        public string Image { get; set; }
        public IFormFile? Photo { get; set; }
    }
}
