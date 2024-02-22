using QomfortHotelFinal.Models;
using System.ComponentModel.DataAnnotations;

namespace QomfortHotelFinal.ViewModels
{
    public class ContactVM
    {
        public Contact? Contact { get; set; }
        //post
        public string Subject { get; set; }
        public string Messages { get; set; }
        public DateTime MessageDate { get; set; }
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
        public int? Rating { get; set; }
    }
}
