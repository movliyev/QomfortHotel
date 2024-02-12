using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QomfortHotelFinal.ViewModels.Account
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Name daxil edilmelidir")]
        [MinLength(3, ErrorMessage = "3 den az simvol olmaz")]
        [MaxLength(25, ErrorMessage = "25 den cox simvol olmaz")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Surname daxil edilmelidir")]
        [MinLength(3, ErrorMessage = "3 den az simvol olmaz")]
        [MaxLength(25, ErrorMessage = "25 den cox simvol olmaz")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "UserName daxil edilmelidir")]
        [MinLength(4, ErrorMessage = "4 den az simvol olmaz")]
        [MaxLength(25, ErrorMessage = "25 den cox simvol olmaz")]
        public string UserName { get; set; }
        
        [NotMapped]
        public IFormFile? Photo { get; set; }
        [Required(ErrorMessage = "Gender secilmelidir")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Email daxil edilmelidir")]
        [MaxLength(256, ErrorMessage = "256 den cox simvol olmaz")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Phone daxil edilmelidir")]
        [MaxLength(100, ErrorMessage = "100 den cox simvol olmaz")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Password daxil edilmelidir")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Password daxil edilmelidir")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "The passwords are not the same")]
        public string ConfirmPassword { get; set; }
    }
}
