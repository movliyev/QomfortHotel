﻿

namespace Hotel_Managment.Domain.Entities
{
    public class RoomFacility
    {
        public int Id { get; set; }

        public int RoomId { get; set; }
        public Room Room { get; set; }
        public int FacilityId { get; set; }
        public Facility Facility { get; set; }
    }
}