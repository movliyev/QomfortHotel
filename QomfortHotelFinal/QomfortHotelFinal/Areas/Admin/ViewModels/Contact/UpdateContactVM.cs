using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QomfortHotelFinal.Areas.Admin.ViewModels.Contact
{
    public class UpdateContactVM
    {
       
        public string? HoverImage { get; set; }
        [MaxLength(100, ErrorMessage = "No more than 100 characters")]
        [MinLength(4, ErrorMessage = "Be less than 4 characters")]
        public string Title { get; set; }
        [MaxLength(100, ErrorMessage = "No more than 100 characters")]
        [MinLength(4, ErrorMessage = "Be less than 4 characters")]
        public string Location { get; set; }
        [MaxLength(256, ErrorMessage = "No more than 256 characters")]
        [MinLength(4, ErrorMessage = "Be less than 4 characters")]
        [DataType(DataType.EmailAddress)]   
        public string Email { get; set; }
        [DataType(DataType.PhoneNumber)]   
        [MaxLength(256, ErrorMessage = "No more than 256 characters")]
        [MinLength(4, ErrorMessage = "Be less than 4 characters")]
        public string PhoneNumber { get; set; }
        [NotMapped]
        public IFormFile? Photo { get; set; }
    }
}
