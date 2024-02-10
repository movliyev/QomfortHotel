using System.ComponentModel.DataAnnotations;

namespace QomfortHotelFinal.ViewModels.Account
{
    public class LoginVM
    {
        [Required(ErrorMessage = "UserName ve ya Email daxil edilmelidir")]
        [MinLength(4, ErrorMessage = "4 den az simvol olmaz")]
        [MaxLength(25, ErrorMessage = "25 den cox simvol olmaz")]
        public string UserNameOrEmail { get; set; }

        [Required(ErrorMessage = "Password daxil edilmelidir")]
        [MinLength(8, ErrorMessage = "8 den az simvol olmaz")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsRemembered { get; set; }
    }
}
