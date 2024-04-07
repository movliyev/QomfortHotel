
using QomfortHotelFinal.Models;
using System.ComponentModel.DataAnnotations;

namespace QomfortHotelFinal.ViewModels
{
    public class RoomVM
    {
        public Room? Room { get; set; }
        public List<RoomImage>? RoomaImage { get; set; }
        public bool? Status { get; set; }
        [Required(ErrorMessage = "A Personcount must be included")]
        
        public int PersonCount { get; set; }
        [Required(ErrorMessage = "A Children must be included")]
       
        public int Children { get; set; }
        [Required(ErrorMessage = "A ArrivalFate must be included")]
        [DataType(DataType.DateTime)]   
        public DateTime ArrivalDate { get; set; }
        [Required(ErrorMessage = "A DeparturDate must be included")]
        [DataType(DataType.DateTime)]
        public DateTime DeparturDate { get; set; }
        public DateTime? ReservationDate { get; set; }

        public List<DateTime>? ReservationDates { get; set; }



    }
}
