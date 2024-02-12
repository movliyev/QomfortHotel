namespace QomfortHotelFinal.Models
{
    public class Servicee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public List<RoomFacility>? RoomServicees { get; set; }

    }
}
