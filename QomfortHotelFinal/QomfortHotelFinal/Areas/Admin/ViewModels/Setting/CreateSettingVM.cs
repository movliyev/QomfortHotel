using System.ComponentModel.DataAnnotations;

namespace QomfortHotelFinal.Areas.Admin.ViewModels.Setting
{
    public class CreateSettingVM
    {
        [Required(ErrorMessage = "A Key must be included")]
        [MaxLength(100, ErrorMessage = "No more than 100 characters")]
        [MinLength(3, ErrorMessage = "No less than 3 characters")]
        public string Key { get; set; }
        [Required(ErrorMessage = "A Value must be included")]
        [MaxLength(100, ErrorMessage = "No more than 100 characters")]
        [MinLength(3, ErrorMessage = "No less than 3 characters")]
        public string Value { get; set; }
    }
}
