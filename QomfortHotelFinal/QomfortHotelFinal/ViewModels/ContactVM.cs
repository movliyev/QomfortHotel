using QomfortHotelFinal.Models;
using System.ComponentModel.DataAnnotations;

namespace QomfortHotelFinal.ViewModels
{
    public class ContactVM
    {
        public Contact? Contact { get; set; }
        //post
        [Required(ErrorMessage = "A Subject must be included")]
        [MaxLength(100, ErrorMessage = "No more than 100 characters")]
        [MinLength(4, ErrorMessage = "Be less than 4 characters")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "A Message must be included")]
        [MaxLength(256, ErrorMessage = "No more than 256 characters")]
        [MinLength(4, ErrorMessage = "Be less than 4 characters")]
        public string Messages { get; set; }
        public DateTime MessageDate { get; set; }
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
        public int? Rating { get; set; }
    }
}


