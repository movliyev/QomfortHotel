namespace QomfortHotelFinal.Models
{
    public class RoomService
    {
        public int Id { get; set; }

        public int RoomId { get; set; }
        public Room Room { get; set; }
        public int ServiceeId { get; set; }
        public Servicee Servicee { get; set; }
    }
}
