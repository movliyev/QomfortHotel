
using QomfortHotelFinal.Models;

namespace QomfortHotelFinal.ViewModels
{
    public class RoomVM
    {
        public List<Room> Rooms { get; set; }
        public Room Room { get; set; }
        public Reservation Reservation { get; set; }
        public int RoomId { get; set; }

        public double? TotalPrice { get; set; }
        public int PersonCount { get; set; }
        public int Children { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DeparturDate { get; set; }
        public DateTime? ReservationDate { get; set; }
    }
}
