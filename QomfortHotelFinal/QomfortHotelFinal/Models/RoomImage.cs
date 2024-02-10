namespace QomfortHotelFinal.Models
{
    public class RoomImage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public bool? IsPrimary { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
    }
}
