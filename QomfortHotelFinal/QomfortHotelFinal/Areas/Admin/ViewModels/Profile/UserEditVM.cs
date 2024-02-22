using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QomfortHotelFinal.Areas.Admin.ViewModels
{
    public class UserEditVM
    {
        [MaxLength(100, ErrorMessage = "No more than 100 characters")]
        [MinLength(3, ErrorMessage = "Be less than 3 characters")]
        public string name { get; set; }
        [MaxLength(100, ErrorMessage = "No more than 100 characters")]
        [MinLength(3, ErrorMessage = "Be less than 3 characters")]
        public string surname { get; set; }
        [DataType(DataType.PhoneNumber)]   
        public string phonenumber { get; set; }
        public string image { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
