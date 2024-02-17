using System.ComponentModel.DataAnnotations;

namespace QomfortHotelFinal.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        //public double? TotalPrice { get; set; }
        public int PersonCount { get; set; }
        public int Children { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DeparturDate { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public bool? Status { get; set; }
        public DateTime? ReservationDate { get; set; }

    }
}
