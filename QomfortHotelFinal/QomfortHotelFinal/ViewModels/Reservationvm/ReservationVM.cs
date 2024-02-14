using QomfortHotelFinal.Models;

namespace QomfortHotelFinal.ViewModels.Reservationvm
{
    public class ReservationVM
    {
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public string Description { get; set; }
        public int Children { get; set; }

        public int PersonCount { get; set; }
        public bool IsActive { get; set; }
        public DateTime ArrivalDateTime { get; set; }
        public DateTime DeparturDateTime { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }


        //public DateTime FirstDay { get; set; }

        //public DateTime LastDay { get; set; }

        public DateTime ReservationDate { get; set; }

        //public string Note { get; set; }

        //public string Name { get; set; }

        //public string Phone { get; set; }


        //public bool PerNight { get; set; }

        public double? TotalPrice { get; set; }
    }
}
