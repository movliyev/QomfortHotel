using System.ComponentModel.DataAnnotations;

namespace QomfortHotelFinal.Areas.Admin.ViewModels.Setting
{
    public class UpdateSettingVM
    {
        [MaxLength(100, ErrorMessage = "No more than 100 characters")]
        [MinLength(3, ErrorMessage = "No less than 3 characters")]
        public string Key { get; set; }
        [MaxLength(100, ErrorMessage = "No more than 100 characters")]
        [MinLength(3, ErrorMessage = "No less than 3 characters")]
        public string Value { get; set; }
    }
}
