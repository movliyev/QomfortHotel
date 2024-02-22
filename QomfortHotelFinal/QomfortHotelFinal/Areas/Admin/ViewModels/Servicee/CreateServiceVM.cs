using System.ComponentModel.DataAnnotations;

namespace QomfortHotelFinal.Areas.Admin.ViewModels
{
    public class CreateServiceVM
    {
        [Required(ErrorMessage = "A Name must be included")]
        [MaxLength(100, ErrorMessage = "No more than 100 characters")]
        [MinLength(3, ErrorMessage = "No less than 3 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "A Description must be included")]
        [MaxLength(256, ErrorMessage = "No more than 256 characters")]
        [MinLength(3, ErrorMessage = "No less than 3 characters")]
        public string Description { get; set; }
        [Required(ErrorMessage = "A RoomTitle must be included")]
        [MaxLength(60, ErrorMessage = "No more than 60 characters")]
        [MinLength(3, ErrorMessage = "No less than 3 characters")]
        public string Icon { get; set; }
    }
}
