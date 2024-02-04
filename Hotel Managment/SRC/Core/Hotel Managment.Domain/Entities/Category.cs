


using Hotel_Managment.Domain.Entities.Common;

namespace Hotel_Managment.Domain.Entities
{
    public class Category:BaseNameable
    {
        public int Id { get; set; }
        public List<Room>? Rooms { get; set; }

    }
}
