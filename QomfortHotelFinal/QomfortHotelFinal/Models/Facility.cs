namespace QomfortHotelFinal.Models
{
    public class Facility
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<RoomFacility>? RoomFacilities { get; set; }
    }
}
