using System.ComponentModel.DataAnnotations;

namespace QomfortHotelFinal.Areas.Admin.ViewModels.Setting
{
    public class UpdateSettingVM
    {
        [Required(ErrorMessage = "Add daxil edilmelidir")]
        [MaxLength(25, ErrorMessage = "25 den uzun simvol olmaz")]
        public string Key { get; set; }
        [Required(ErrorMessage = "Add daxil edilmelidir")]
        [MaxLength(25, ErrorMessage = "25 den uzun simvol olmaz")]
        public string Value { get; set; }
    }
}
