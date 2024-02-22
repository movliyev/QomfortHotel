using System.ComponentModel.DataAnnotations;

namespace QomfortHotelFinal.Areas.Admin.ViewModels
{
  
        public class UpdateFacilityVM
        {

           [MaxLength(100, ErrorMessage = "No more than 100 characters")]
           [MinLength(4, ErrorMessage = "Be less than 4 characters")]
           public string Name { get; set; }
        }
    
}
