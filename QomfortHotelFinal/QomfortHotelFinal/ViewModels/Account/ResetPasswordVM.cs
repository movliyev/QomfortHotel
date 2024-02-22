using System.ComponentModel.DataAnnotations;

namespace QomfortHotelFinal.ViewModels.Account
{
    public class ResetPasswordVM
    {
        [Required(ErrorMessage = "A Password must be included")]
        [MaxLength(100, ErrorMessage = "No more than 100 characters")]
        [MinLength(8, ErrorMessage = "Be less than 8 characters")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "A Password must be included")]
        [MaxLength(100, ErrorMessage = "No more than 100 characters")]
        [MinLength(8, ErrorMessage = "Be less than 8 characters")]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword))]
        public string ConfirmPassword { get; set; }
    }
}
