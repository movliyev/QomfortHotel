

using Hotel_Managment.Domain.Entities.Common;

namespace Hotel_Managment.Domain.Entities
{
    public class Facility : BaseNameable
    {
        public int Id { get; set; }
        public List<RoomFacility>? RoomFacilities { get; set; }
    }
}
