namespace QomfortHotelFinal.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Room>? Rooms { get; set; }
    }
}
