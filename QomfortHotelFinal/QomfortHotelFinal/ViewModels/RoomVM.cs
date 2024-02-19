
using QomfortHotelFinal.Models;
using System.ComponentModel.DataAnnotations;

namespace QomfortHotelFinal.ViewModels
{
    public class RoomVM
    {
        public Room? Room { get; set; }

        public bool? Status { get; set; }
        [Required]
        public int PersonCount { get; set; }
        public int Children { get; set; }
        [Required]
        public DateTime ArrivalDate { get; set; }
        [Required]
        public DateTime DeparturDate { get; set; }
        public DateTime? ReservationDate { get; set; }

        public List<DateTime>? ReservationDates { get; set; }

    }
}
