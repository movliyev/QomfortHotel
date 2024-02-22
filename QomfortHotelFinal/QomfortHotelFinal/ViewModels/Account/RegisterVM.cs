using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QomfortHotelFinal.ViewModels.Account
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "A Name must be included")]
        [MaxLength(256, ErrorMessage = "No more than 256 characters")]
        [MinLength(3, ErrorMessage = "Be less than 3 characters")]
      
        public string Name { get; set; }
        [Required(ErrorMessage = "A Surname must be included")]
        [MaxLength(256, ErrorMessage = "No more than 256 characters")]
        [MinLength(3, ErrorMessage = "Be less than 3 characters")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "A  UserName must be included")]
        [MaxLength(256, ErrorMessage = "No more than 256 characters")]
        [MinLength(4, ErrorMessage = "Be less than 4 characters")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "A Gender must be included")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "A  Email must be included")]
        [MaxLength(256, ErrorMessage = "No more than 256 characters")]
        [MinLength(4, ErrorMessage = "Be less than 4 characters")]
        [DataType(DataType.PhoneNumber)]
        public string Email { get; set; }
        [Required(ErrorMessage = "A  PhoneNumber must be included")]
        [MaxLength(256, ErrorMessage = "No more than 256 characters")]
        [MinLength(3, ErrorMessage = "Be less than 3 characters")]
        [DataType(DataType.PhoneNumber)]

        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "A Password must be included")]
        [MaxLength(100, ErrorMessage = "No more than 100 characters")]
        [MinLength(8, ErrorMessage = "Be less than 8 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "A Password must be included")]
        [MaxLength(100, ErrorMessage = "No more than 100 characters")]
        [MinLength(8, ErrorMessage = "Be less than 8 characters")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]  
        public string ConfirmPassword { get; set; }
    }
}
