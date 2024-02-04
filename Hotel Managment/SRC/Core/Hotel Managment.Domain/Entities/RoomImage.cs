

using Hotel_Managment.Domain.Entities.Common;

namespace Hotel_Managment.Domain.Entities
{
    public class RoomImage : BaseNameable
    {
        public int Id { get; set; }

        public string Url { get; set; }
        public bool? IsPrimary { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
    }
}
