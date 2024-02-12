namespace QomfortHotelFinal.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public string  Description { get; set; }
      
        public int PersonCount { get; set; }
        public bool IsActive { get; set; }
        public DateTime ArrivalDateTime { get; set; }
        public DateTime DeparturDateTime { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
        //public string ArriveDate => ArrivalDateTime.ToString("MV/dd/yyy");
        //public string ArrivalTime => ArrivalDateTime.ToString("hh/mm/tt");
        //public string DepartureDate => DeparturDateTime.ToString("MV/dd/yyy");
        //public string DepartureTime => DeparturDateTime.ToString("hh/mm/tt");
    }
}
