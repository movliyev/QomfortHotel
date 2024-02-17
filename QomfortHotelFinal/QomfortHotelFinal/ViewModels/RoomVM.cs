
using QomfortHotelFinal.Models;

namespace QomfortHotelFinal.ViewModels
{
    public class RoomVM
    {
        public Room? Room { get; set; }
      
        public int? RoomId { get; set; }
        public bool Status { get; set; }
        public int PersonCount { get; set; }
        public int Children { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DeparturDate { get; set; }
        public DateTime? ReservationDate { get; set; }

        public List<DateTime>? ReservationDates { get; set; } // Oda için mevcut rezervasyon tarihleri

    }
}
