using System.ComponentModel.DataAnnotations;

namespace QomfortHotelFinal.Areas.Admin.ViewModels
{
    public class CreateFacilityVM
    {
        [Required(ErrorMessage = "Add daxil edilmelidir")]
        [MaxLength(25, ErrorMessage = "25 den uzun simvol olmaz")]
        public string Name { get; set; }
    }
}
