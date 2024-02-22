using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QomfortHotelFinal.Areas.Admin.ViewModels
{
    public class CreateSlideVM
    {
        [Required(ErrorMessage = "A RoomTitle must be included")]
        [MaxLength(100, ErrorMessage = "No more than 100 characters")]
        [MinLength(3, ErrorMessage = "No less than 3 characters")]
        public string Title { get; set; }
        [Required(ErrorMessage = "A RoomTitle must be included")]
        [MaxLength(100, ErrorMessage = "No more than 100 characters")]
        [MinLength(3, ErrorMessage = "No less than 3 characters")]
        public string Description { get; set; }
        [Required(ErrorMessage = "A Photo must be included")]
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
