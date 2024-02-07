



namespace Hotel_Managment.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Room>? Rooms { get; set; }

    }
}
