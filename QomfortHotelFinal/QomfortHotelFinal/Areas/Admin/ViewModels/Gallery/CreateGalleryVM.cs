using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QomfortHotelFinal.Areas.Admin.ViewModels.Gallery
{
    public class CreateGalleryVM
    {

        [Required(ErrorMessage = "A ImageName must be included")]
        [MaxLength(100, ErrorMessage = "No more than 100 characters")]
        [MinLength(4, ErrorMessage = "Be less than 4 characters")]

        public string Name { get; set; }
        [Required(ErrorMessage = "A Photo must be included")]
        [NotMapped]
        public IFormFile? Photo { get; set; }
    }
}
