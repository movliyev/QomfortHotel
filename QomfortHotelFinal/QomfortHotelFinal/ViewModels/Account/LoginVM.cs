using System.ComponentModel.DataAnnotations;

namespace QomfortHotelFinal.ViewModels.Account
{
    public class LoginVM
    {
        [Required(ErrorMessage = "A Email and Username must be included")]
        [MaxLength(256, ErrorMessage = "No more than 256 characters")]
        [MinLength(4, ErrorMessage = "Be less than 4 characters")]
       
        public string UserNameOrEmail { get; set; }

        [Required(ErrorMessage = "A Password must be included")]
        [MaxLength(100, ErrorMessage = "No more than 100 characters")]
        [MinLength(8, ErrorMessage = "Be less than 8 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsRemembered { get; set; }
    }
}
