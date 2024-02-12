using System.ComponentModel.DataAnnotations;

namespace QomfortHotelFinal.ViewModels
{
    public class ForgotPasswordVM
    {
        [Required, MaxLength(256, ErrorMessage = "No more than 256 characters"), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
