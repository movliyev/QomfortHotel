using System.ComponentModel.DataAnnotations;

namespace QomfortHotelFinal.ViewModels
{
    public class ForgotPasswordVM
    {
        [Required(ErrorMessage = "A Email must be included")]
        [MaxLength(256, ErrorMessage = "No more than 256 characters")]
        [MinLength(4, ErrorMessage = "Be less than 4 characters")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
