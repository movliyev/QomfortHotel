namespace QomfortHotelFinal.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DetailDescription { get; set; }
        public decimal Price { get; set; }
        public int Size { get; set; }
        public int Bed { get; set; }
        public int Capacity { get; set; }
        public int CategoryId { get; set; }
        public List<RoomImage>? RoomImages { get; set; }

        public int BathRoom { get; set; }
        public Category Category { get; set; }
        public List<RoomFacility> RoomFacilities { get; set; }
        public bool? Status { get; set; }
    }
}
